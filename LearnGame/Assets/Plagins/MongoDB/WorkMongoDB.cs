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
    private static bool isConnection = false;
    public static bool Connection()
    {
        try
        {
            if (!isConnection)
            {
                client = new MongoClient("mongodb+srv://admin:0987654321@gameclaster.zzp3f.mongodb.net/GameClaster?retryWrites=true&w=majority");
                database = client.GetDatabase("TestDatabase");
                isConnection = true;
            }
        }
        catch
        {
            isConnection = false;
        }
        return isConnection;
    }


    public static string GetData(string name, string path)
    {
        return GetJson(name);
    }

    public static string GetJson(string name)
    {
        var collection = database.GetCollection<BsonDocument>("data_json");
        var filter = new BsonDocument() { { "name", name } };
        var jsonFormat = collection.Find(filter).FirstOrDefault();
        return jsonFormat is null ? null : jsonFormat.GetValue(2).ToJson();
    }

    public static void GetImages(string name, string path)
    {
        var gridFS = new GridFSBucket(database);
        var collection = database.GetCollection<BsonDocument>("data_image");
        var filter = new BsonDocument() { { "father_name", name } };
        var nameImages = new List<string>();
        collection.Find(filter).ForEachAsync(x => nameImages.Add(x.GetValue(2).AsString));
        foreach (var nameImage in nameImages)
        {
            var file = new FileStream(path + "\\" + nameImage, FileMode.OpenOrCreate);
            gridFS.DownloadToStreamByNameAsync(nameImage, file);
        }
    }
}
