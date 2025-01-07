namespace WiseReminder.Infrastructure.Caching;

public sealed class QuoteConverter : JsonConverter<Quote>
{
    public override void WriteJson(
        JsonWriter writer,
        Quote? value,
        JsonSerializer serializer)
    {
        if (value == null)
        {
            return;
        }

        writer.WriteStartObject();

        writer.WritePropertyName("Id");
        writer.WriteValue(value.Id);

        writer.WritePropertyName("Text");
        writer.WriteValue(value.Text.Value);

        writer.WritePropertyName("QuoteDate");
        writer.WriteValue(value.QuoteDate.Value.ToString("o"));

        writer.WritePropertyName("AuthorId");
        writer.WriteValue(value.AuthorId);

        writer.WritePropertyName("CategoryId");
        writer.WriteValue(value.CategoryId);

        writer.WriteEndObject();
    }

    public override Quote ReadJson(
        JsonReader reader,
        Type objectType,
        Quote? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var id = Guid.Empty;
        var textValue = string.Empty;
        var quoteDateValue = default(DateOnly);
        var authorId = Guid.Empty;
        var categoryId = Guid.Empty;

        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.EndObject)
            {
                break;
            }

            if (reader.TokenType == JsonToken.PropertyName)
            {
                var propertyName = reader.Value?.ToString();
                reader.Read();

                switch (propertyName)
                {
                    case "Id":
                        id = Guid.Parse(reader.Value?.ToString() ?? string.Empty);
                        break;
                    case "Text":
                        textValue = reader.Value?.ToString();
                        break;
                    case "QuoteDate":
                        quoteDateValue =
                            DateOnly.Parse(reader.Value?.ToString() ?? string.Empty);
                        break;
                    case "AuthorId":
                        authorId = Guid.Parse(reader.Value?.ToString() ?? string.Empty);
                        break;
                    case "CategoryId":
                        categoryId = Guid.Parse(reader.Value?.ToString() ?? string.Empty);
                        break;
                }
            }
        }

        var text = new Text(textValue!);
        var date = Date.Create(quoteDateValue).Value;

        var quote = (Quote)Activator.CreateInstance(
            typeof(Quote),
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            [text, date, authorId, categoryId],
            null
        )!;

        var idField = typeof(Entity<Quote>).GetField("<Id>k__BackingField",
            BindingFlags.Instance | BindingFlags.NonPublic);

        idField!.SetValue(quote, id);

        return quote;
    }
}