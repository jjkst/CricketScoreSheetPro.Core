﻿using Firebase.Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            var val = new ObservableCollection<T>();
            foreach (var item in firebaseObjectCollection)
            {
                val.Add(item.Object);
            }

            return val;
        }
    }
}
