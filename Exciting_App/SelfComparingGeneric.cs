namespace SelfComparingGenericNamespace {
    public class SelfComparingGeneric {
        string id;
        string[] attributes;
        string[] FileOneData;
        string[] FileTwoData;
        string FileOneName;
        string FileTwoName;
        public SelfComparingGeneric(string name, string FileOneNameParameter, string FileTwoNameParameter, int size) {
            id = name;
            FileOneData = new string[size];
            FileTwoData = new string[size];
            attributes = new string[size];
            attributes = FillArray(attributes);
            FileOneName = FileOneNameParameter;
            FileTwoName = FileTwoNameParameter;

        }
        public string[] FillArray(string[] arr) {
            for (int i = 0; i < arr.Length; i++) {
                arr[i] = "";
            }
            return arr;
        }
        public void AddToOne(string value, int index) {
            FileOneData[index] = value;
            if(FileTwoData[index] == null) {
                FileTwoData[index] = "NULL";
            }
        }
        public void AddToTwo(string value, int index) {
            FileTwoData[index] = value;
            if(FileOneData[index] == null) {
                FileOneData[index] = "NULL";
            }
        }
        public string Compare() {
            string results = "";
            for (int i = 0; i < FileOneData.Length; i++) { //FileOneData.Length
                if(!FileOneData[i].Equals(FileTwoData[i])) {
                    results += FileOneName + " has attribute " + id + " as " + FileOneData[i] + " while " + FileTwoName + " has it as " + FileTwoData[i] + "<br>";
                }
            }
            return results;
        }
    }
}