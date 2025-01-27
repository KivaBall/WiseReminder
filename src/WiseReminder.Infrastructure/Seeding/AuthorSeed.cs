namespace WiseReminder.Infrastructure.Seeding;

public static class AuthorSeed
{
    public const string Sql =
        """
        INSERT INTO authors (id, name, biography, birth_date, death_date, added_at, is_deleted)
        VALUES ('6dc8eed6-643b-4b74-bf37-353dbab335ab', 'Friedrich Nietzsche',
        N'Friedrich Nietzsche was a German philosopher, cultural critic, composer, poet, and philologist. Known for his critiques of traditional European morality and religion, and his ideas of the Übermensch and eternal recurrence.',
        '1844-10-15', '1900-08-25', '2025-01-01', false),
        ('71c12634-9cd3-419e-853d-2d3d687d5915', 'Sigmund Freud',
        'Sigmund Freud was an Austrian neurologist and the founder of psychoanalysis. He is renowned for his theories on the unconscious mind, the mechanisms of repression, and the structure of personality.',
        '1856-05-06', '1939-09-23', '2025-01-01', false),
        ('c5d6b99e-b2d5-41db-833d-76129cdf85b8', 'William Shakespeare',
        N'William Shakespeare was an English playwright, poet, and actor. Widely regarded as the greatest writer in the English language and the world’s greatest dramatist.',
        '1564-04-23', '1616-04-23', '2025-01-01', false),
        ('e648367f-9b3b-403d-b30c-f3829171a96d', 'Dalai Lama',
        'The Dalai Lama is a title given to the spiritual leader of the Tibetan people. Known for promoting compassion, mindfulness, and interfaith dialogue worldwide.',
        '1935-07-06', NULL, '2025-01-01', false),
        ('da9b3aae-2481-4d2f-803d-a0272bc77deb', 'Abraham Lincoln',
        'Abraham Lincoln was the 16th President of the United States. He is best remembered for his leadership during the American Civil War and his efforts to end slavery in the United States.',
        '1809-02-12', '1865-04-15', '2025-01-01', false),
        ('f6ed3f2f-c791-452a-b2a9-0a3eac67eabb', 'Marcus Aurelius',
        'Marcus Aurelius was a Roman emperor and philosopher of Stoicism. His work `Meditations` remains a cornerstone of Stoic philosophy.',
        '121-04-26', '180-03-17', '2025-01-01', false),
        ('aeff5d50-a10d-4ede-8068-b13f82b46540', 'Elon Musk',
        'Elon Musk is an entrepreneur and business magnate. He is the founder of SpaceX, Tesla, Neuralink, and other companies, and is known for his vision of sustainable energy and space exploration.',
        '1971-06-28', NULL, '2025-01-01', false),
        ('887fe481-4c4b-4beb-b988-4e2ebb0a9956', 'Steve Jobs',
        'Steve Jobs was an American business magnate and co-founder of Apple Inc. He revolutionized the technology industry with innovations such as the iPhone and Macintosh.',
        '1955-02-24', '2011-10-05', '2025-01-01', false),
        ('b40b601e-5b53-4ac7-81b2-7c1e3efc9220', 'Albert Einstein',
        'Albert Einstein was a German-born theoretical physicist who developed the theory of relativity, one of the two pillars of modern physics. He received the Nobel Prize in Physics in 1921.',
        '1879-03-14', '1955-04-18', '2025-01-01', false),
        ('dd562d5f-939a-4438-8351-f79aa4d09225', 'Winston Churchill',
        'Winston Churchill was a British statesman, soldier, and writer who served as Prime Minister of the United Kingdom during World War II. He is renowned for his speeches and leadership during the war.',
        '1874-11-30', '1965-01-24', '2025-01-01', false),
        ('73614b77-710a-4659-8564-618ea9480803', 'Mark Twain',
        'Mark Twain was an American writer, humorist, entrepreneur, publisher, and lecturer. He is best known for his novels `The Adventures of Tom Sawyer` and `Adventures of Huckleberry Finn`.',
        '1835-11-30', '1910-04-21', '2025-01-01', false),
        ('aa942d39-e364-45a6-88c0-c178c7ace5af', 'Oscar Wilde',
        'Oscar Wilde was an Irish poet and playwright. He is best remembered for his sharp wit, his novel `The Picture of Dorian Gray`, and his numerous plays.',
        '1854-10-16', '1900-11-30', '2025-01-01', false),
        ('06421c9f-e7b3-4f58-bfe1-c388c96d6074', 'Leonardo da Vinci',
        'Leonardo da Vinci was an Italian polymath of the Renaissance period. Known for his works such as `Mona Lisa` and `The Last Supper`, as well as his contributions to science and engineering.',
        '1452-04-15', '1519-05-02', '2025-01-01', false),
        ('0092c1ff-28b1-4e72-93b6-3e74ef563b51', 'Vincent van Gogh',
        'Vincent van Gogh was a Dutch post-impressionist painter who is among the most famous and influential figures in Western art. His works include `Starry Night` and `Sunflowers`.',
        '1853-03-30', '1890-07-29', '2025-01-01', false);
        """;
}