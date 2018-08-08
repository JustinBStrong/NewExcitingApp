using System;
using System.Collections.Generic;

namespace Exciting_App {
    public class AppToNodeRelation {
        string gsiId;
        HashSet<string> relations;
        public AppToNodeRelation(string id, string node) {
            gsiId = id;
            relations = new HashSet<string>();
            relations.Add(node);
        }

        public Boolean AddNode(string node) {
            if (relations.Contains(node)) {
                return false;
            } else {
                relations.Add(node);
                return true;
            }
        }
        public string GetGsiId() {
            return gsiId;
        }
        public Boolean HasNode(string node) {
            return relations.Contains(node);
        }
        public HashSet<string> GetNodeList() {
            return relations;
        }
        public string GetAppToNodeList() {
            IEnumerator<string> enumerator = relations.GetEnumerator();
            string list = gsiId + "\t\t<br>";
            while(enumerator.MoveNext()) {
                list += "\t\t" + enumerator.Current + "<br>";
            }
            return list;
        }
    }
}