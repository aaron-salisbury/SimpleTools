using SimpleTools.Core.Base.Helpers;
using SimpleTools.Core.Domains.FlatUIColorPicker;
using SimpleTools.Core.Domains.ToDoListManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace SimpleTools.Core.DataAccess
{
    public class CRUD
    {
        private const string TO_DO_LIST_FILE_NAME = "ToDoList.json";

        public static List<FlatColor> ReadAllFlatColors()
        {
            XDocument flatColorsXDoc = new XDocument();
            flatColorsXDoc = XDocument.Parse(GetEmbeddedResourceText("SimpleTools.Core.DataAccess.FlatColors.xml"));

            return flatColorsXDoc.Element("FlatColors")
                .Descendants()
                .Select(fc => new FlatColor() { Name = fc.Value, Hex = fc.Attribute("hex").Value })
                .ToList();
        }

        public static async Task UpdateToDoListAsync(IEnumerable<ListCategory> listCategories)
        {
            string json = await Json.StringifyAsync(listCategories);

            await WriteLocalDataFileAsync(TO_DO_LIST_FILE_NAME, json);
        }

        public static async Task<IEnumerable<ListCategory>> ReadToDoListAsync()
        {
            string json = await ReadLocalDataFileAsync(TO_DO_LIST_FILE_NAME);

            if (!string.IsNullOrEmpty(json))
            {
                return await Json.ToObjectAsync<IEnumerable<ListCategory>>(json);
            }
                
            return null;
        }

        private static string GetEmbeddedResourceText(string filename)
        {
            string result = string.Empty;

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
            using (StreamReader streamReader = new StreamReader(stream))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }

        private static async Task WriteLocalDataFileAsync(string fileName, string contents)
        {
            // Requires references to the following:
            // 1) "Windows.Foundation.FoundationContract.winmd" probably stored at
            //        C:\Program Files (x86)\Windows Kits\10\References\{some version}\Windows.Foundation.FoundationContract\4.0.0.0
            // 2) "System.Runtime.WindowsRuntime.dll" probably stored at
            //        C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5
            // 3) "Windows.WinMD" probably stored at
            //        C:\Program Files (x86)\Windows Kits\10\UnionMetadata\Facade

            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, contents);
        }

        private static async Task<string> ReadLocalDataFileAsync(string fileName)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(file);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }
}
