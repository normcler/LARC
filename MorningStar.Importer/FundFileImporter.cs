using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CsvHelper;

namespace Morningstar.Importer
{
    public class FundFileImporter
    {
        /// <summary>
        ///     Purpose: Import fund data from a csv file from Morningstar
        /// </summary>
        /// <param name="repoLocation">
        ///     The location of the fund file repository.
        /// </param>
        /// <param name="ticker">
        ///     The name of the file.
        /// </param>
        /// <returns>
        ///     A List of class Holding
        /// </returns>
        /// <remarks>
        ///     Programmer: N. S. Clerman
        /// 
        ///     Revisions:
        ///     1)  N. S. Clerman, 28-Jul-2017: Read the file in with a single
        ///         operation.
        ///     2)  N. S. Clerman, 29-Jul-2017: Employ a method to form the file
        ///         name.
        /// </remarks>
        public static List<Holding> Import (string repoLocation, string ticker)
        {
            // return variable
            List<Holding> result = new List<Holding>();

            // get the fully-qualified file name.
            string filename = repoLocation;

            using (StreamReader reader = File.OpenText(filename))
            {
                var fund_csv = new CsvReader(reader);
                fund_csv.Configuration.RegisterClassMap<HoldingMap>();
                fund_csv.Configuration.HasHeaderRecord = true;
                // Code to read the file record by record.
                /*while (fund_csv.Read())
                {
                    Holding record = fund_csv.GetRecord<Holding>();
                    result.Add(record);
                }*/

                // Code to read all the records. (This requires Linq)
                result = fund_csv.GetRecords<Holding>().ToList();
                return result;
            }
        }

        /// <summary>
        ///     Purpose: Form the fully-qualified filename for the Morningstar
        ///              fund file
        /// </summary>
        /// <param name="repoLocation">
        ///     The location of the file repository
        /// </param>
        /// <param name="ticker">
        ///     The fund ticker
        /// </param>
        /// <returns name="fileName">
        ///     The fully-qualified name
        /// </returns>
        /// <remarks>
        ///     Programmer: N. S. Clerman
        /// </remarks>
        private static string fullyQualifiedFileName(string repoLocation,
            string ticker)
        {

            const string MORNINGSTAR_FILE_PREFIX = "Holdings_";
            const string CSV_FILE_EXTENT = ".csv";

            string fileName = repoLocation + MORNINGSTAR_FILE_PREFIX + ticker +
                CSV_FILE_EXTENT;
            return fileName;
        }
    }
}
