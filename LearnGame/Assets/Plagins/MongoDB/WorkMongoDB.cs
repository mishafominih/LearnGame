using System.Collections;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class WorkMogoDb : MonoBehaviour
{ 

    public static MongoClient client;
    public static IMongoDatabase database;
    public static void Connection()
    {
        client = new MongoClient("mongodb+srv://admin:0987654321@gameclaster.zzp3f.mongodb.net/GameClaster?retryWrites=true&w=majority");
        database = client.GetDatabase("TestDatabase");
    }

    public static string GetData(string name, string path)
    {
        //GetImages(name, path);
        return GetJson(name);
    }

    private static string GetJson(string name)
    {
        var collection = database.GetCollection<BsonDocument>("data_json");
        var bson = new BsonDocument() { { "name", name } };
        FilterDefinition<BsonDocument> filter = bson;
        var jsonFormat = collection.Find(filter).FirstOrDefault().GetValue(2).ToJson();
        return jsonFormat;
    }

    public static void GetImages(string name, string path)
    {
        var gridFS = new GridFSBucket(database);
        var collection = database.GetCollection<BsonDocument>("data_image");
        var filter = new BsonDocument() { { "father_name", name } };
        var nameImages = new List<string>();
        collection.FindSync(filter).ForEachAsync(x => nameImages.Add(x.GetValue(2).AsString));
        foreach (var nameImage in nameImages)
        {
            var file = new FileStream(path + "\\" + nameImage, FileMode.OpenOrCreate);
            gridFS.DownloadToStreamByNameAsync(nameImage, file);
        }
    }
}
