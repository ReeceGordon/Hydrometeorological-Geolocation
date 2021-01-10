using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hydrometeorological_Geolocation_Prototype_V2.Client;
using Hydrometeorological_Geolocation_Prototype_V2.Server;
using Hydrometeorological_Geolocation_Prototype_V2.Shared;
using Hydrometeorological_Geolocation_Prototype_V2.Server.Controllers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace HydroGeoUnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        private readonly ILogger<FavouriteObjectsController> FavouriteLogger;
        private readonly ILogger<IndexObjectsController> IOLogger;

        [TestMethod]
        public void TestFavPostMethod()
        {
            FavouriteObjects testObject = new FavouriteObjects
            {
                Location = "TestLocation"
            };
            FavouriteObjectsController testFav = new FavouriteObjectsController(FavouriteLogger);

            testFav.Post(testObject);

            bool expected = true;

            bool result = false;

            IEnumerable<FavouriteObjects> storedPost = testFav.Get();

            using (var sequenceEnum = storedPost.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    Console.WriteLine(sequenceEnum.Current.Location);
                    if(sequenceEnum.Current.Location.Contains("TestLocation"))
                    {
                        result = true;
                        break;
                    }
                }
            }

            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void TestFavGetMethod()
        {
            FavouriteObjectsController testFav = new FavouriteObjectsController(FavouriteLogger);
            IEnumerable<FavouriteObjects> storedPost;
            storedPost = testFav.Get();

            bool expected = true;

            bool result = false;

            using (var sequenceEnum = storedPost.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    Console.WriteLine(sequenceEnum.Current.Location);
                    if (sequenceEnum.Current.Location.Split(' ')[0] == ("TestLocation"))
                    {
                        result = true;
                        break;
                    }
                }
            }

            Assert.IsNotNull(storedPost);
            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void TestFavDelMethod()
        {
            FavouriteObjectsController testFav = new FavouriteObjectsController(FavouriteLogger);
            IEnumerable<FavouriteObjects> storedPost;
            storedPost = testFav.Get();
            String storedLoc = "";

            int count = 0;

            using (var sequenceEnum = storedPost.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    count += 1;
                }
            }

            testFav.Delete(storedLoc);

            int count2 = 0;
            using (var sequenceEnum = storedPost.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    count2 += 1;
                }
            }

            Assert.AreEqual(count, count2);

        }

        [TestMethod]
        public void TestIOGetMethod()
        {
            IndexObjectsController testFav = new IndexObjectsController(IOLogger);
            IEnumerable<IndexObjects> storedPost;
            storedPost = testFav.Get();

            int count = 0;
            int count2 = 0;
            IndexObjects dummyObject;
            using (var sequenceEnum = storedPost.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    count += 1;
                    if (sequenceEnum.Current.Date.ToShortDateString() == "12/08/2019")
                    {
                        dummyObject = sequenceEnum.Current;
                        testFav.Post(dummyObject);
                        break;
                    }

                }
            }


            using (var sequenceEnum = storedPost.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    count2 += 1;
                    if (sequenceEnum.Current.Date.ToShortDateString() == "12/08/2019")
                    {
                        dummyObject = sequenceEnum.Current;
                        break;
                    }

                }
            }

            Assert.AreEqual(count, count2);
        }

        [TestMethod]
        public void TestIOPostMethod()
        {
            IndexObjectsController testFav = new IndexObjectsController(IOLogger);
            IEnumerable<IndexObjects> storedPost;
            storedPost = testFav.Get();

            bool expected = true;
            bool actual = false;

            using (var sequenceEnum = storedPost.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    if (sequenceEnum.Current.Date.ToShortDateString() == "12/08/2019")
                    {
                        actual = true;
                        break;
                    }

                }
            }

            Assert.AreEqual(expected, actual);

        }




    }
}
