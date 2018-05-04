using System;

namespace HackfestBotBase.Models
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DataStoreEntryAttribute : Attribute
    {
        public DataStore DataStore { get; }
        public string FriendlyName { get; }

        public DataStoreEntryAttribute(string friendlyName, DataStore dataStore)
        {
            DataStore = dataStore;
            FriendlyName = friendlyName;
        }
    }
}