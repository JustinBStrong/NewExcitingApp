using System;
using System.Collections.Generic;

namespace Exciting_App {
    public class AppToNodeRelationDataHolder {
        int DuplicateCount = 0;
        int MissingCount = 0;
        public Dictionary<string, AppToNodeRelation> dictionary;
        string badAdds;
        public AppToNodeRelationDataHolder(Dictionary<string, AppToNodeRelation> inDictionary, string b) {
            dictionary = inDictionary;
            badAdds = b;
        }
        public string GetBadAdds() {
            return badAdds;
        }

        public Dictionary<string, AppToNodeRelation> GetDictionary() {
            return dictionary;
        }
    }
}
