using Firebase.Database;
using System.Collections.Generic;

namespace CricketScoreSheetPro.Core.Helper
{
    public class Common
    {
        public static string BallsToOversValueConverter(int balls)
        {
            return balls/6 + "." + balls%6;
        }

        public static IList<T> ConvertFirebaseObjectCollectionToList<T>(IReadOnlyCollection<FirebaseObject<T>> firebaseObjectCollection) where T : class
        {
            var val = new List<T>();
            foreach (var item in firebaseObjectCollection)
            {
                val.Add(item.Object);
            }

            return val;
        }
    }
}
