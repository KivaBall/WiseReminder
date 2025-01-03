namespace WiseReminder.Infrastructure.Seeding;

public static class QuoteSeed
{
    public const string Sql =
        @"INSERT INTO Quotes (Id, Text, AuthorId, CategoryId, QuoteYear, QuoteMonth, QuoteDay, AddedAt, IsDeleted)
        VALUES ('24c8e0ad-5edc-4967-87e3-649881a5a8fe',
        'Success is not the key to happiness. Happiness is the key to success. If you love what you are doing, you will be successful.',
        '6dc8eed6-643b-4b74-bf37-353dbab335ab', '6602da7d-efcf-4f2e-bdca-628354349446', 1956, 10, 12, '11/30/2024', 0),
       ('565c9471-c2ed-42e4-a923-09794b333a7f',
        'Success usually comes to those who are too busy to be looking for it.',
        '6dc8eed6-643b-4b74-bf37-353dbab335ab', 'e7f4a9b2-6b3a-4a1f-8c65-d17e3b9a2b3c', 1970, 5, 4, '11/30/2024', 0),
       ('2ca1d40a-a2b8-450f-b201-87303ac4f095',
        'The road to success is always under construction.',
        '6dc8eed6-643b-4b74-bf37-353dbab335ab', 'c0f7d8e9-9e87-4c2f-9d98-f4c3d0e5f7e9', 1963, 2, 20, '11/30/2024', 0),
       ('ab1bbccd-fc5c-4f44-95bb-84a3f38cac51',
        'Creativity takes courage and a willingness to fail. Only then can you succeed.',
        '71c12634-9cd3-419e-853d-2d3d687d5915', 'f3a0b2c3-c2a9-4f5f-9fbc-f7c6d3f8a0b2', 1958, 7, 15, '11/30/2024', 0),
       ('c4572f58-619c-482d-a9de-f5295f14f790',
        'Leadership is not about being in charge. It is about taking care of those in your charge.',
        '71c12634-9cd3-419e-853d-2d3d687d5915', 'e2f9f0a1-b198-4e4f-9fab-f6c5d2e7f9a1', 1960, 3, 25, '11/30/2024', 0),
       ('5fadbb11-4f1a-4823-a018-015f55831b1c',
        'The unexamined life is not worth living.',
        '71c12634-9cd3-419e-853d-2d3d687d5915', '85a2bd6e-a3f3-4301-9353-5fe480b99362', 1963, 12, 10, '11/30/2024', 0),
       ('93a651a8-2d69-4f0f-9746-57c4931572cc',
        'Happiness is not something ready-made. It comes from your own actions.',
        'c5d6b99e-b2d5-41db-833d-76129cdf85b8', 'f0a2cbe1-5a2e-4e35-b77e-9f52c1e9a4d1', 1935, 6, 15, '11/30/2024', 0),
       ('7595e8e8-6007-438b-a205-522c2609d3da',
        'A friend is one who overlooks your broken fence and admires the flowers in your garden.',
        'c5d6b99e-b2d5-41db-833d-76129cdf85b8', 'd6e3f7b0-7f65-49b7-bc23-123f57f8a4c5', 1927, 11, 22, '11/30/2024', 0),
       ('e6dfa667-8175-460e-9534-3a0d2702c142',
        'Life is what happens when you’re busy making other plans.',
        'c5d6b99e-b2d5-41db-833d-76129cdf85b8', 'a8f5b3c4-7c65-48a1-9b76-f2c1b8d3e5f7', 1949, 2, 18, '11/30/2024', 0),
       ('620e6f68-a1ed-4d34-bca2-593b0429216b',
        'Happiness is the secret to all beauty; there is no beauty without happiness.',
        'e648367f-9b3b-403d-b30c-f3829171a96d', 'f0a2cbe1-5a2e-4e35-b77e-9f52c1e9a4d1', 1942, 10, 14, '11/30/2024', 0),
       ('449f625d-0cc7-4a97-893b-4415a80cff4c',
        'Wisdom begins in wonder.',
        'e648367f-9b3b-403d-b30c-f3829171a96d', 'c1d2ad58-2cfa-4a8d-91d8-e2c1d36f7d2f', 1929, 5, 22, '11/30/2024', 0),
       ('8f52f3ac-ec02-4f2c-9e9a-14344a33c611',
        'Creativity is seeing what everyone else has seen and thinking what no one else has thought.',
        'e648367f-9b3b-403d-b30c-f3829171a96d', 'f3a0b2c3-c2a9-4f5f-9fbc-f7c6d3f8a0b2', 1951, 11, 18, '11/30/2024', 0);";
}