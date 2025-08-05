using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Momon.Biju.Web.TagHelpers;

[HtmlTargetElement("custom-file-input")]
public class CustomFileInputTagHelper : TagHelper
{
    [HtmlAttributeName("prop-name")]
    public ModelExpression PropName { get; set; }

    [HtmlAttributeName("existing-image")]
    public string ExistingImage { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var id = PropName.Name.Replace(".", "_");
        var name = PropName.Name;
        var previewId = $"preview_{id}";

        output.TagName = "div";
        output.Attributes.SetAttribute("class", "file-upload");

        var html = $$"""
            <label for="{{id}}" class="custom-file-label">Escolha a imagem</label>
            <input type="file" id="{{id}}" name="{{name}}" class="custom-file-input" accept="image/*" />
            <small class="text-muted d-block mt-1">
                Formatos suportados: JPG, PNG. Tamanho máximo: 2MB.
            </small>
            <span id="file-name-{{id}}" class="file-name">
                Nenhum arquivo selecionado
            </span>
            <div>
                <img id="{{previewId}}" src="{{(string.IsNullOrEmpty(ExistingImage) ? "" : ExistingImage)}}" alt="Pré-visualização da imagem" style="max-width: 200px; margin-top: 10px; {{(string.IsNullOrEmpty(ExistingImage) ? "display:none;" : "")}}" />
            </div>
            
            <script>
                (function () {
                    const fileInput = document.getElementById('{{id}}');
                    const fileNameSpan = document.getElementById('file-name-{{id}}');
                    const previewImage = document.getElementById('{{previewId}}');
                    fileInput.addEventListener('change', function () {
                        const file = this.files[0];
                        fileNameSpan.textContent = file ? file.name : 'Nenhum arquivo selecionado';
                        if (file && file.type.startsWith('image/')) {
                            const reader = new FileReader();
                            reader.onload = function (e) {
                                previewImage.src = e.target.result;
                                previewImage.style.display = 'block';
                            };
                            reader.readAsDataURL(file);
                        } else {
                            previewImage.style.display = 'none';
                        }
                    });
                })();
            </script>
                             
        """;

        output.Content.SetHtmlContent(html);
    }
}
