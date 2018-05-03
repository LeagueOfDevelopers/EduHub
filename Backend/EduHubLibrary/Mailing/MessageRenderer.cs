using System;
using System.IO;
using System.Linq;
using RazorLight;

namespace EduHubLibrary.Mailing
{
    public class MessageRenderer
    {
        public static string RenderPartialToString(object model)
        {
            var modelType = model.GetType().ToString().Split('.').Last();

            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplates");
            var templateAbsolutePath = Path.Combine(templatePath, modelType + ".cshtml");
            var template = File.ReadAllText(templateAbsolutePath);

            var engine = new RazorLightEngineBuilder().UseMemoryCachingProvider().Build();
            var renderedText = engine.CompileRenderAsync(modelType, template, model).Result;

            return renderedText;
        }
    }
}