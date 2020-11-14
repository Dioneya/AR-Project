using UnityEngine;
using System.IO;

public class UserConfig : MonoBehaviour
{
    private static readonly string _path = Path.Combine(Application.persistentDataPath, "userConfig.dat");

    public static void ReadUser() 
    {
        if (File.Exists(_path)) 
        {
            using (BinaryReader reader = new BinaryReader(File.Open(_path, FileMode.Open)))
            {
                GlobalVariables.tokenResponse = new TokenClass.TokenResponse
                {
                    token_type = reader.ReadString(),
                    expires_in = reader.ReadInt32(),
                    access_token = reader.ReadString(),
                    refresh_token = reader.ReadString()
                };
            }
        }
    }

    public static void WriteUser() 
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(_path, FileMode.OpenOrCreate)))
        {
            writer.Write(GlobalVariables.tokenResponse.token_type);
            writer.Write(GlobalVariables.tokenResponse.expires_in);
            writer.Write(GlobalVariables.tokenResponse.access_token);
            writer.Write(GlobalVariables.tokenResponse.refresh_token);
        }
    } 
}
