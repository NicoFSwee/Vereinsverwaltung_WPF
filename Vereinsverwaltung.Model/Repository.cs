using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using Vereinsverwaltung.Model;

namespace Vereinsverwaltung.Model
{
    /// <summary>
    /// Repository als Singleton, damit die Daten aus dem CSV-File nur einmal gelesen werden!
    /// </summary>
    public class Repository
    {
        private const string fileName = "Mitglieder.csv";
        private const int LASTNAME_IDX = 0;
        private const int FIRSTNAME_IDX = 1;
        private const int DOB_IDX = 2;

        private static Repository instance;

        List<Member> members;

        private Repository()
        {
            members = new List<Member>();
            LoadCdsFromCsv();
        }

        public static Repository GetInstance()
        {
            if (instance == null)
                instance = new Repository();

            return instance;
        }

        /// <summary>
        /// Lädt die Daten vom csv-File in die Collection
        /// </summary>
        private void LoadCdsFromCsv()
        {
            string[][] membersCsv = fileName.ReadStringMatrixFromCsv(true);

            members = membersCsv.Select(m => new Member
            {
                FirstName = m[FIRSTNAME_IDX],
                LastName = m[LASTNAME_IDX],
                DateOfBirth = DateTime.Parse(m[DOB_IDX])
            }).ToList();
        }

        /// <summary>
        /// Neues Mitglied in die Collection einfügen
        /// </summary>
        /// <param name="member"></param>
        public void AddMember(Member member)
        {
            members.Add(member);
        }

        /// <summary>
        /// Liefert eine (neue!) Liste aller Mitglieder
        /// Entkoppelt die zurückgelieferte Collektion von der Collection im Repository
        /// Beachte! Die Objekte selbst sind jedoch noch dieselben (clonen wäre erforderlich)!
        /// </summary>
        /// <returns></returns>
        public List<Member> GetAllMembers()
        {
            return members.OrderBy(x => x.LastName).ToList(); //Erstellt neue Liste!
        }

        public void DeleteMember(Member member)
        {
            members.Remove(member);
        }
    }
}