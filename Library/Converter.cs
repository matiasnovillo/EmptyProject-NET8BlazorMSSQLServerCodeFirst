using System.Reflection;
using System.Text;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace EmptyProject.Library
{
    public static class Converter
    {
        /// <returns>A byte array with UTF-8 encoding</returns>
        public static byte[] StringToByteArray(string String)
        {
            try
            {
                if (true)
                {
                    return Encoding.UTF8.GetBytes(String); 
                }
            }
            catch (Exception) { throw; }
        }

        /// <returns>A string with UTF-8 encoding</returns>
        public static string ByteArrayToString(byte[] ByteArray)
        {
            try
            {
                return Encoding.UTF8.GetString(ByteArray);
            }
            catch (Exception) { throw; }
        }

        /// <returns>A byte array with UTF-8 encoding</returns>
        public static byte[] IntToByteArray(int Int)
        {
            try
            {
                return Encoding.UTF8.GetBytes(Int.ToString());
            }
            catch (Exception) { throw; }
        }

        public static int BoolToInt(bool Bool)
        {
            if (Bool == true) { return 1; }
            else { return 0; }
        }

        /// <returns>An int with UTF-8 encoding</returns>
        public static int ByteArrayToInt(byte[] ByteArray)
        {
            try
            {
                return Convert.ToInt32(Encoding.UTF8.GetString(ByteArray));
            }
            catch (Exception) { throw; }
        }

        public static object CopyObjectProperties(object OriginalObject, object DestinyObject)
        {
            try
            {
                if (OriginalObject == null || DestinyObject == null) { throw new Exception("Parameters can't be null"); }

                bool HasPropertiesToCopy = false;
                PropertyInfo[] ListDestinyObjectProperties = DestinyObject.GetType().GetProperties();

                foreach (PropertyInfo destinyObjectProperty in ListDestinyObjectProperties)
                {
                    PropertyInfo? OriginalProperty = OriginalObject.GetType().GetProperty(destinyObjectProperty.Name);
                    
                    if (OriginalProperty != null)
                    {
                        if (OriginalProperty.CanWrite)
                        {
                            destinyObjectProperty.SetValue(DestinyObject, OriginalProperty.GetValue(OriginalObject, null), null);
                            HasPropertiesToCopy = true;
                        }
                    }
                }
                if (HasPropertiesToCopy)
                { return DestinyObject; }
                else
                { throw new Exception("The properties of original object are null or can't be written"); }
            }
            catch (Exception) { throw; }
        }

        public static object SetObjectPropertiesToNull(object Object)
        {
            try
            {
                bool HasPropertiesToModifiy = false;
                PropertyInfo[] ListObjectProperties = Object.GetType().GetProperties();
                foreach (PropertyInfo objectProperty in ListObjectProperties)
                {
                    if (objectProperty != null)
                    {
                        if (objectProperty.CanWrite)
                        {
                            objectProperty.SetValue(Object, null, null);
                        }                    
                    }
                }
                if (HasPropertiesToModifiy)
                { return Object; }
                else
                { throw new Exception("The properties of original object are null or can't be written"); }
            }
            catch (Exception) { throw; }
        }
    }

    public class JsonToTimeSpan : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString() ?? "";
            return TimeSpan.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
