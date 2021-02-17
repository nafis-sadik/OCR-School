//using System;
//using System.Collections.Generic;
//using System.Text;
//using Entities.Models;
////using Entities.GCP;

//namespace Repository
//{
//    class DBQuery
//    {
//        readonly OCR_DBContext dB;
//        public IList<int> DBlist;
//        DBQuery()
//        {
//            dB = new OCR_DBContext();
//        }
//        public void Insert()
//        {
//            for (int i = 0; i <= ms.Question.Count; i++)
//            {
//                DBlist[i] = ms.Question[i];

//            }

//            /*  ***INSERT***
//               Cars c1 = new Cars()
//                {
                    
//                    Id = 18,
//                    Name = "BMW",
//                    Price = 33333

//                };

//                Subject s1 = new Subject()
//                {
//                    IdSubject = 3,
//                    SubjectName = "Chemistry"
//                };

//                dB.Cars.Add(c1);
//                dB.Subject.Add(s1);
//                dB.SaveChanges(); 
//            */

//            /*  ***UPADATE ***

//            Cars cUpdate = dBContext.Cars.Find(18);
//            cUpdate.Price = 99999;
//            dBContext.SaveChanges();
//            */
//            /* *** DELETE***
//            Cars cDelete = dBContext.Cars.Find(18);
//            dBContext.Cars.Remove(cDelete);
//            dBContext.SaveChanges();
//            */

//        }

//        public void Read() { }
//    }
//}
