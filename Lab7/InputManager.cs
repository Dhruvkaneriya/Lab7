// https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio

using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

using Lab7.Materials;

namespace Lab7.IOManagers
{
    public static class InputManager
    {
        public static Model InitializeModel(string path)
        {
            JObject modelData = LoadJson(path);
            // This model can only handle 1 material
            Material material = GetMaterial(modelData["materials"][0]);
            Model model = GetModel(material, modelData["settings"]);
            AddSensors(model, modelData["sensors"]);
            AddCells(model, modelData["cells"]);
            return model;
        }
        private static JObject LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                JObject modelData = JObject.Parse(json);
                return modelData;
            }
        }

        private static void AddCells(Model m, JToken cellData)
        {
            throw new System.NotImplementedException();
        }

        private static void AddSensors(Model m, JToken sensorData)
        {
            throw new System.NotImplementedException();
        }

        private static Model GetModel(Material material, JToken settingsData)
        {
            throw new System.NotImplementedException();
        }

        private static Material GetMaterial(JToken materialData)
        {
            var dData = GetDispersionData(materialData["d_data"]);
            var rData = GetRelaxationData(materialData["r_data"]);
            return new Material(dData, rData);
        }

        private static DispersionData GetDispersionData(JToken dData)
        {
            var WMaxLa = (double)dData["max_freq_la"];
            var WMaxTa = (double)dData["max_freq_ta"];
            var laData = dData["la_data"].ToObject<double[]>();
            var taData = dData["ta_data"].ToObject<double[]>();
            return new DispersionData(laData, WMaxLa, taData, WMaxTa);
        }

        private static RelaxationData GetRelaxationData(JToken rData)
        {
            var Bl = (double)rData["Bl"];
            var Btn = (double)rData["Btn"];
            var Btu = (double)rData["Btu"];
            var BI = (double)rData["BI"];
            var W = (double)rData["W"];
            return new RelaxationData(Bl, Btn, Btu, BI, W);
        }
        
    }
}
