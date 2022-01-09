using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Tiber_Browser.Classes
{
    internal class DataAccess
    {
        internal async void CreateSettingsFile()
        {
            try
            {
                // Create an xml settings file
                // If the file already exists, it will throw an exception and not create the file.
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("Settings.xml", CreationCollisionOption.FailIfExists);

                // Set the permissions for the stream to read and write
                using (IRandomAccessStream writeStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    // create a write stream and create settings for the writer.
                    Stream s = writeStream.AsStreamForWrite();
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Async = true;
                    settings.Indent = true;  // for easier reading

                    // pass in the stream and settings to the writer and actually create the settings file async.
                    using(XmlWriter writer = XmlWriter.Create(s, settings))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("settings");
                        writer.WriteStartElement("history");
                        writer.WriteEndElement();
                        writer.WriteStartElement("bookmarks");
                        writer.WriteEndElement();
                        writer.WriteStartElement("searchengine");
                        writer.WriteStartElement("bing");
                        writer.WriteAttributeString("prefix", "https://www.bing.com/search?q="); // prefix for bing
                        writer.WriteEndElement();
                        writer.WriteStartElement("google");
                        writer.WriteAttributeString("prefix", "https://www.google.com/search?q="); // prefix for google
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Flush();
                        await writer.FlushAsync();
                    }
                }
                await Windows.System.Launcher.LaunchFileAsync(storageFile);
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
    }
}
