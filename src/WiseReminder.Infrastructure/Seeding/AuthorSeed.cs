namespace WiseReminder.Infrastructure.Seeding;

public static class AuthorSeed
{
    public const string Sql =
        @"INSERT INTO authors (id, name, biography, birth_date, death_date, added_at, is_deleted)
        VALUES ('6dc8eed6-643b-4b74-bf37-353dbab335ab', 'Charmaine Tasseler',
        'Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Etiam vel augue. Vestibulum rutrum rutrum neque. Aenean auctor gravida sem. Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio. Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo.',
        '1934-06-07', '1976-05-07', '2024-11-30', false),
       ('71c12634-9cd3-419e-853d-2d3d687d5915', 'Devina Schultes',
        'Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum. Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est. Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.',
        '1943-11-26', '1968-05-29', '2024-11-30', false),
       ('c5d6b99e-b2d5-41db-833d-76129cdf85b8', 'Zena Ethersey',
        'Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci. Mauris lacinia sapien quis libero.',
        '1900-11-03', '1954-04-28', '2024-11-30', false),
       ('e648367f-9b3b-403d-b30c-f3829171a96d', 'Lavinia Denmead',
        'Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl. Aenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum. Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est. Phasellus sit amet erat. Nulla tempus.',
        '1905-03-27', '1957-12-25', '2024-11-30', false),
       ('da9b3aae-2481-4d2f-803d-a0272bc77deb', 'Grenville O''Dunneen',
        'Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante. Nulla justo. Aliquam quis turpis eget elit sodales scelerisque. Mauris sit amet eros. Suspendisse accumsan tortor quis turpis. Sed ante.',
        '1941-10-20', '1975-05-02', '2024-11-30', false),
       ('f6ed3f2f-c791-452a-b2a9-0a3eac67eabb', 'Gottfried Lummus',
        'Praesent lectus. Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis. Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus. Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci.',
        '1918-11-09', '1969-07-04', '2024-11-30', false),
       ('aeff5d50-a10d-4ede-8068-b13f82b46540', 'Guinna Maulden',
        'In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet. Maecenas leo odio, condimentum id, luctus nec, molestie sed, justo. Pellentesque viverra pede ac diam. Cras pellentesque volutpat dui. Maecenas tristique, est et tempus semper, est quam pharetra magna, ac consequat metus sapien ut nunc.',
        '1922-01-22', '1990-07-29', '2024-11-30', false),
       ('887fe481-4c4b-4beb-b988-4e2ebb0a9956', 'Nealy Bowkett',
        'Nullam varius. Nulla facilisi. Cras non velit nec nisi vulputate nonummy. Maecenas tincidunt lacus at velit. Vivamus vel nulla eget eros elementum pellentesque.',
        '1942-02-21', '1970-01-16', '2024-11-30', false),
       ('b40b601e-5b53-4ac7-81b2-7c1e3efc9220', 'Constantia Wathey',
        'Morbi a ipsum. Integer a nibh. In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.',
        '1905-05-12', '1958-03-15', '2024-11-30', false),
       ('dd562d5f-939a-4438-8351-f79aa4d09225', 'Javier Zimmer',
        'Pellentesque ultrices mattis odio. Donec vitae nisi. Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus. Curabitur at ipsum ac tellus semper interdum. Mauris ullamcorper purus sit amet nulla.',
        '1927-10-17', '1991-04-25', '2024-11-30', false);";
}