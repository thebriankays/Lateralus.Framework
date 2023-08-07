namespace Lateralus.Framework;

public static class Utility
{
    public class LookupItem
    {
        public long? Value { get; set; }

        public string Name { get; set; } = String.Empty;
    }

    public static IList<LookupItem> EnumToList<T>() => Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(
                v =>
                {
                    string name = Enum.GetName(typeof(T), v) ?? String.Empty;

                    MemberInfo memberInfo = typeof(T).GetMember(name).FirstOrDefault();

                    if (memberInfo != null)
                    {
                        DescriptionAttribute descriptionAttribute = (DescriptionAttribute)memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault();

                        if (descriptionAttribute != null)
                        {
                            name = descriptionAttribute.Description;
                        }
                    }

                    return new LookupItem
                    {
                        Name = name,
                        Value = (long?)Convert.ChangeType(v, TypeCode.Int64)
                    };
                })
            .ToList();

    /// <summary>
    /// A method that creates a zip file
    /// </summary>
    /// <param name="fileToCompress">the file to compress</param>
    /// <param name="zipFileName">the zip file name</param>
    public static void CreateZipFile(string fileToCompress, string zipFileName)
    {
        // create the zip file
        using (ICSharpCode.SharpZipLib.Zip.ZipFile zip = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(zipFileName))
        {
            zip.NameTransform = new ICSharpCode.SharpZipLib.Zip.ZipNameTransform(Path.GetDirectoryName(zipFileName));

            // initialize the file so that it can accept updates
            zip.BeginUpdate();

            // add the file to the zip file
            zip.Add(fileToCompress);

            // commit the update once we are done
            zip.CommitUpdate();

            // close the file
            zip.Close();
        }
    }

    /// <summary>
    /// A method to extract a file from a zip file
    /// </summary>
    /// <param name="zipFileName"></param>
    /// <param name="fileToExtract"></param>
    public static void ExtractZipFile(string zipFileName, string fileToExtract)
    {
        // load the zip file
        using (ICSharpCode.SharpZipLib.Zip.ZipFile zip = new(zipFileName))
        {
            var zipEntry = zip.GetEntry(fileToExtract);
            if (zipEntry == null)
            {
                throw new ArgumentException(fileToExtract, "Can't find file 'fileToExtract' in Zip file 'zipFileName'.");
            }

            using (var zipStream = zip.GetInputStream(zipEntry))
            {
                // get the zip file path 
                string zipFilePath = System.IO.Path.GetDirectoryName(zipFileName);

                byte[] buffer = new byte[4096];     // 4K is optimum
                                                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                                                    // of the file, but does not waste memory.
                using (FileStream streamWriter = File.Create(System.IO.Path.Combine(zipFilePath, fileToExtract)))
                {
                    ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(zipStream, streamWriter, buffer);
                }
            }
        }
    }

    /// <summary>
    /// Formats the SSN to ###-##-#### or returns the input if it isn't in the ######### format.
    /// </summary>
    /// <param name="ssn"></param>
    /// <returns></returns>
    public static string FormatSSN(string ssn)
    {
        if (ssn == null)
        {
            return String.Empty;
        }

        System.Text.RegularExpressions.Regex regex = new(@"^\d{9}$");
        if (regex.IsMatch(ssn))
        {
            return String.Format("{0:000-00-0000}", ssn.To<int>());
        }
        else
        {
            return ssn;
        }
    }

    /// <summary>
    /// Mask given SSN
    /// </summary>
    /// <param name="ssnToMask"></param>
    /// <returns>Masked SSN</returns>
    public static string MaskSSN(string ssnToMask)
    {
        string maskedSSN;

        if (ssnToMask.Trim().Length > 0)
        {
            maskedSSN = ssnToMask.Trim().PadLeft(11, '#'); 
            maskedSSN = "###-##-" + maskedSSN.Substring(7, 4);
        }
        else
        {
            maskedSSN = ssnToMask;
        }

        return maskedSSN;
    }
}
