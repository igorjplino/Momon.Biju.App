using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Momon.Biju.Web.TagHelpers;

[HtmlTargetElement("custom-file-input")]
public class CustomFileInputTagHelper : TagHelper
{
    [HtmlAttributeName("prop-name")]
    public ModelExpression PropName { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var id = PropName.Name.Replace(".", "_");
        var name = PropName.Name;

        output.TagName = "div";
        output.Attributes.SetAttribute("class", "file-upload");

        var label = $$"""
            <label for="{{id}}" class="custom-file-label">Escolha a imagem</label>
            <input type="file" id="{{id}}" name="{{name}}" class="custom-file-input" />
            <span id="file-name-{{id}}" class="file-name">Nenhum arquivo selecionado</span>
            <span class="text-danger field-validation-valid" data-valmsg-for="{{name}}" data-valmsg-replace="true"></span>

            <script>
            document.getElementById('{{id}}').addEventListener('change', function () {
                const fileName = this.files.length > 0 ? this.files[0].name : "Nenhum arquivo selecionado";
                document.getElementById('file-name-{{id}}').textContent = fileName;
            });
            </script>
        """;

        output.Content.SetHtmlContent(label);
    }
}