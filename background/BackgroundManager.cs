using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace UV_Quant
{
    /// <summary>
    /// Manages the retrieval/setting of current backgrounds
    /// </summary>
    class BackgroundManager
    {
        /// <summary>
        /// Contains a mapping between the library name and the background DataMatrix
        /// that is cached for it. 
        /// </summary>
        private Hashtable ht_backgrounds = new Hashtable();
        /// <summary>
        /// mode indicates the various library modes --> predefined vs on file
        /// </summary>
        private string mode = null;

        String back_data_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\backgrounddata";

        public BackgroundManager(Boolean load_info)
        {
            //ensure the background data folder exists
            if (!Directory.Exists(back_data_path))
            {
                Directory.CreateDirectory(back_data_path);
            }
            //we shouldn't load the information from disk unless needed
            if (load_info)
            {
                //First off load any saved backgrounds
                XmlSerializer serializer = new XmlSerializer(typeof(BackgroundInfo));
                StreamReader reader = null;
                try
                {
                    reader = new StreamReader(back_data_path + "\\BackGroundInfo.xml");
                }
                catch (Exception ex)
                {
                    //nothin to do, we just couldn't simply retrieve the 
                    //background information...we probably never saved it
                }
                if (reader != null)
                {
                    BackgroundInfo background_info = (BackgroundInfo)serializer.Deserialize(reader);
                    mode = background_info.CachedMode;
                    CachedLibrary[] libraries = background_info.CachedLibraries;
                    foreach (CachedLibrary library in libraries)
                    {
                        string type = library.CachedType;
                        string library_name = library.Name;
                        string file_name = library.CachedBackgroundFile;
                        DataMatrix background_data = new DataMatrix(new FileInfo(back_data_path + "\\" + file_name));
                        ht_backgrounds.Add(library_name, background_data);
                    }
                }
            }

        }

        /// <summary>
        /// Retrieves a cached background for a given library. 
        /// If there is no saved backround, it will return <code>null</code>
        /// </summary>
        /// <param name="library_name"></param>
        /// <param name="library_mode"></param>
        /// <returns></returns>
        public DataMatrix getCachedBackGround(string library_name, int library_mode)
        {
            DataMatrix back_ground = null;

            if (library_name != null && this.mode == library_mode.ToString())
            {
                back_ground = (DataMatrix)ht_backgrounds[library_name];
            }
            return back_ground;
        }

        /// <summary>
        /// Persists the background data passed through <code>backgrounds</code> to file. 
        /// Here, we are assuming that the sequence of backgrounds as they appear in <code>backgrounds</code>
        /// is exactly the same as the way the corresponding libraries appear in <code>libraries</code>. 
        /// 
        /// </summary>
        /// <param name="backgrounds"></param>
        /// <param name="libraries"></param>
        /// <param name="mode"></param>
        public void persistBackgroundInfo(DataMatrix[] backgrounds, LibraryMatrix[] libraries, int mode, string site_name)
        {
            //first clean out all the files inside the directory
            Array.ForEach(Directory.GetFiles(@back_data_path),
                    delegate(string path) { File.Delete(path); });

            BackgroundInfo background_info = new BackgroundInfo();
            background_info.CachedMode = mode.ToString();

            CachedLibrary[] cached_libraries = new CachedLibrary[libraries.Length];
            for (int i = 0; i < libraries.Length; ++i)
            {
                string file_count = (string)backgrounds[i].getHeaderInfo()[DataMatrix.FileHeader.FILE_COUNT];
                string file_date = (string)backgrounds[i].getHeaderInfo()[DataMatrix.FileHeader.DATE];
                string bkgd_file_name = getSingleBeamFileName(file_count, file_date);

                string bkgd_path = getSingleBeamPath(
                    file_count,
                    file_date,
                    site_name);
                File.Copy(bkgd_path, back_data_path + "\\" + bkgd_file_name, true);
                CachedLibrary cached_library = new CachedLibrary();
                cached_library.CachedBackgroundFile = bkgd_file_name;
                //we currently don't use the type
                cached_library.CachedType = "";
                cached_library.Name = libraries[i].library_name;
                cached_libraries[i] = cached_library;
            }
            background_info.CachedLibraries = cached_libraries;
            //now persist information
            XmlSerializer serializer = new XmlSerializer(typeof(BackgroundInfo));
            StreamWriter writer = new StreamWriter(back_data_path + "\\BackGroundInfo.xml");
            serializer.Serialize(writer, background_info);
        }

        /// <summary>
        /// Returns the single beam full path
        /// </summary>
        /// <param name="file_count"></param>
        /// <param name="file_date"></param>
        /// <param name="site_name"></param>
        /// <returns></returns>
        public string getSingleBeamPath(string file_count, string file_date, string site_name)
        {
            DateTime file_time = DateTime.Parse(file_date);
            String full_path = "C:\\" + site_name + "\\" + file_time.Year + "-" + file_time.Month + "-" + file_time.Day + "\\"
                + getSingleBeamFileName(file_count, file_date);
            return full_path;
        }

        /// <summary>
        /// Returns the single beam file name
        /// </summary>
        /// <param name="file_count"></param>
        /// <param name="file_time"></param>
        /// <returns></returns>
        public String getSingleBeamFileName(string file_count, string file_date)
        {
            DateTime file_time = DateTime.Parse(file_date);
            return "UVQuant " +
                file_time.Year + "-" +
                file_time.Month + "-" +
                file_time.Day + " " +
                file_count + ".csv";
        }
    }
}