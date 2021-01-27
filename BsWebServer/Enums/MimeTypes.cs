using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BsWebServer.Variables
{
    public class MimeTypes
    {
        public static string HTML { get; private set; } = "text/html";
        public static string PDF { get; private set; } = "application/pdf";
        public static string ZIP { get; private set; } = "application/zip";
        public static string RAR { get; private set; } = "application/vnd.rar";
        public static string ZIP7 { get; private set; } = "application/x-7z-compressed";
        public static string GZIP { get; private set; } = "application/gzip";
        public static string ICO { get; private set; } = "image/vnd.microsoft.icon";
        public static string CSS { get; private set; } = "text/css";
        public static string CSV { get; private set; } = "text/csv";
        public static string MSDOC { get; private set; } = "application/msword";
        public static string GIF { get; private set; } = "image/gif";
        public static string JAR { get; private set; } = "application/java-archive";
        public static string JPEG { get; private set; } = "image/jpeg";
        public static string JS { get; private set; } = "text/javascript";
        public static string JSON { get; private set; } = "application/json";
        public static string JSONLD { get; private set; } = "application/ld+json";
        public static string MP3 { get; private set; } = "audio/mpeg";
        public static string MPEG { get; private set; } = "video/mpeg";
        public static string PNG { get; private set; } = "image/png";
        public static string TAR { get; private set; } = "application/x-tar";
        public static string XML { get; private set; } = "text/xml";
        public static string BMP { get; private set; } = "image/bmp";
        public static string BIN { get; private set; } = "application/octet-stream";
        public static string TXT { get; private set; } = "text/plain";

        public static string CombineTypes(string[] mimes)
        {
            string final = "";
            foreach(string st in mimes)
            {
                final += st + ",";
            }
            return final;
        }

        public static string getFileMimeType(FileInfo file)
        {
            string ext = file.Extension;
            switch (ext)
            {
                case ".html":
                case ".htm":
                    return HTML;
                case ".pdf":
                    return PDF;
                case ".zip":
                    return ZIP;
                case ".rar":
                    return RAR;
                case ".7z":
                    return ZIP7;
                case ".gz":
                    return GZIP;
                case ".ico":
                    return ICO;
                case ".css":
                    return CSS;
                case ".csv":
                    return CSV;
                case ".doc":
                case ".dot":
                    return MSDOC;
                case ".gif":
                    return GIF;
                case ".jar":
                    return JAR;
                case ".jpg":
                case ".jpeg":
                    return JPEG;
                case ".js":
                    return JS;
                case ".json":
                    return JSON;
                case ".jsonld":
                    return JSONLD;
                case ".mp3":
                    return MP3;
                case ".mpeg":
                    return MPEG;
                case ".png":
                    return PNG;
                case ".tar":
                    return TAR;
                case ".xml":
                    return XML;
                case ".bmp":
                    return BMP;
                case ".bin":
                    return BIN;
                case ".txt":
                default:
                    return TXT;
            }
        }

        public static string getFileMimeType(string file)
        {
            FileInfo fileinf = new FileInfo(file);
            string ext = fileinf.Extension;
            switch (ext)
            {
                case ".html":
                case ".htm":
                    return HTML;
                case ".pdf":
                    return PDF;
                case ".zip":
                    return ZIP;
                case ".rar":
                    return RAR;
                case ".7z":
                    return ZIP7;
                case ".gz":
                    return GZIP;
                case ".ico":
                    return ICO;
                case ".css":
                    return CSS;
                case ".csv":
                    return CSV;
                case ".doc":
                case ".dot":
                    return MSDOC;
                case ".gif":
                    return GIF;
                case ".jar":
                    return JAR;
                case ".jpg":
                case ".jpeg":
                    return JPEG;
                case ".js":
                    return JS;
                case ".json":
                    return JSON;
                case ".jsonld":
                    return JSONLD;
                case ".mp3":
                    return MP3;
                case ".mpeg":
                    return MPEG;
                case ".png":
                    return PNG;
                case ".tar":
                    return TAR;
                case ".xml":
                    return XML;
                case ".bmp":
                    return BMP;
                case ".bin":
                    return BIN;
                case ".txt":
                default:
                    return TXT;
            }
        }
    }
}
