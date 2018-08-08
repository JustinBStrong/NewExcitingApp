using System.Collections.Generic;
public class App {
    public string appGsiId;
    string upstreamList;
    string downstreamList;
    public HashSet<string> upstream;
    public HashSet<string> downstream;
    public App(string id) {
        appGsiId = id;
    }
    public void SetUpstreamList(string list) {
        upstreamList = list;
    }
    public void SetDownstreamList(string list) {
        downstreamList = list;
    }
    public void AddUpstream(string gsiId) {
        if(upstream != null) {
            upstream.Add(gsiId);
        } else {
            upstream = new HashSet<string>();
            upstream.Add(gsiId);
        }
    }
    public void AddDownstream(string gsiId) {
        if (downstream != null) {
            downstream.Add(gsiId);
        }
        else {
            downstream = new HashSet<string>();
            downstream.Add(gsiId);
        }
    }
}