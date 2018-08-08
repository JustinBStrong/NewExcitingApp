using System.Collections.Generic;
using SelfComparingGenericNamespace;
namespace Exciting_App {
    public class GenericData {
        Dictionary<string, SelfComparingGeneric> data = new Dictionary<string, SelfComparingGeneric>();
        string duplicates = "";
        public GenericData() {
            
        }
        public void AddDictionary(Dictionary<string, SelfComparingGeneric> dictionary) {
            data = dictionary;
        }
        public void SetDuplicates(string dupes) {
            duplicates = dupes;
        }
        public Dictionary<string, SelfComparingGeneric> GetDictionary() {
            return data;
        }
    }
}