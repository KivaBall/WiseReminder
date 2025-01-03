namespace WiseReminder.Infrastructure.Seeding;

public static class AuthorSeed
{
    public const string Sql =
        @"INSERT INTO Authors (Id, Name, Biography, BirthYear, BirthMonth, BirthDay, DeathYear, DeathMonth, DeathDay, AddedAt, IsDeleted)
        VALUES ('6dc8eed6-643b-4b74-bf37-353dbab335ab', 'Charmaine Tasseler',
        'Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Etiam vel augue. Vestibulum rutrum rutrum neque. Aenean auctor gravida sem. Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio. Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo.',
        1934, 6, 7, 1976, 5, 7, '11/30/2024', 0),
       ('71c12634-9cd3-419e-853d-2d3d687d5915', 'Devina Schultes',
        'Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum. Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est. Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.',
        1943, 11, 26, 1968, 5, 29, '11/30/2024', 0),
       ('c5d6b99e-b2d5-41db-833d-76129cdf85b8', 'Zena Ethersey',
        'Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci. Mauris lacinia sapien quis libero.',
        1900, 11, 3, 1954, 4, 28, '11/30/2024', 0),
       ('e648367f-9b3b-403d-b30c-f3829171a96d', 'Lavinia Denmead',
        'Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl. Aenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum. Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est. Phasellus sit amet erat. Nulla tempus.',
        1905, 3, 27, 1957, 12, 25, '11/30/2024', 0),
       ('da9b3aae-2481-4d2f-803d-a0272bc77deb', 'Grenville O''Dunneen',
        'Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante. Nulla justo. Aliquam quis turpis eget elit sodales scelerisque. Mauris sit amet eros. Suspendisse accumsan tortor quis turpis. Sed ante.',
        1941, 10, 20, 1975, 5, 2, '11/30/2024', 0),
       ('f6ed3f2f-c791-452a-b2a9-0a3eac67eabb', 'Gottfried Lummus',
        'Praesent lectus. Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis. Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci.',
        1918, 11, 9, 1969, 7, 4, '11/30/2024', 0),
       ('aeff5d50-a10d-4ede-8068-b13f82b46540', 'Guinna Maulden',
        'In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet. Maecenas leo odio, condimentum id, luctus nec, molestie sed, justo. Pellentesque viverra pede ac diam. Cras pellentesque volutpat dui. Maecenas tristique, est et tempus semper, est quam pharetra magna, ac consequat metus sapien ut nunc.',
        1922, 1, 22, 1990, 7, 29, '11/30/2024', 0),
       ('887fe481-4c4b-4beb-b988-4e2ebb0a9956', 'Nealy Bowkett',
        'Nullam varius. Nulla facilisi. Cras non velit nec nisi vulputate nonummy. Maecenas tincidunt lacus at velit. Vivamus vel nulla eget eros elementum pellentesque.',
        1942, 2, 21, 1970, 1, 16, '11/30/2024', 0),
       ('b40b601e-5b53-4ac7-81b2-7c1e3efc9220', 'Constantia Wathey',
        'Morbi a ipsum. Integer a nibh. In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.',
        1905, 5, 12, 1958, 3, 15, '11/30/2024', 0),
       ('dd562d5f-939a-4438-8351-f79aa4d09225', 'Javier Zimmer',
        'Pellentesque ultrices mattis odio. Donec vitae nisi. Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus. Curabitur at ipsum ac tellus semper interdum. Mauris ullamcorper purus sit amet nulla.',
        1927, 10, 17, 1991, 4, 25, '11/30/2024', 0);";
}