﻿namespace WiseReminder.Infrastructure.Seeding;

public static class CategorySeed
{
    public const string Sql =
        """
        INSERT INTO categories (id, name, description, added_at, is_deleted)
        VALUES
        ('85a2bd6e-a3f3-4301-9353-5fe480b99362', 'Philosophy',
        'This category features quotes that explore deep questions about existence, knowledge, ethics, and the nature of reality. It includes insights from renowned philosophers across different schools of thought.',
        '2025-01-01', false),
        ('6602da7d-efcf-4f2e-bdca-628354349446', 'Motivation',
        'Quotes in this category aim to inspire action, perseverance, and personal growth. They encourage individuals to overcome challenges, stay determined, and pursue their goals with passion and resilience.',
        '2025-01-01', false),
        ('a6e2fca9-0787-4658-bd6c-90f7ad3b4cfc', 'Love',
        N'The Love category offers quotes that reflect on the beauty, complexity, and power of love in all its forms—romantic, familial, and universal. These quotes inspire compassion, empathy, and connection.',
        '2025-01-01', false),
        ('f0a2cbe1-5a2e-4e35-b77e-9f52c1e9a4d1', 'Happiness',
        'Quotes that celebrate the essence of joy, gratitude, and the small moments that make life beautiful. They encourage a positive outlook and appreciation of the present.',
        '2025-01-01', false),
        ('c1d2ad58-2cfa-4a8d-91d8-e2c1d36f7d2f', 'Wisdom',
        'This category gathers timeless insights from various cultures, thinkers, and traditions, offering profound reflections on life, decision-making, and personal growth.',
        '2025-01-01', false),
        ('d6e3f7b0-7f65-49b7-bc23-123f57f8a4c5', 'Friendship',
        'Quotes that explore the depth and value of friendship, trust, and mutual understanding, celebrating the bonds that connect us to others.',
        '2025-01-01', false),
        ('e7f4a9b2-6b3a-4a1f-8c65-d17e3b9a2b3c', 'Success',
        'Focused on achievement, ambition, and overcoming obstacles, this category highlights the principles and mindsets that drive success in all areas of life.',
        '2025-01-01', false),
        ('a8f5b3c4-7c65-48a1-9b76-f2c1b8d3e5f7', 'Life',
        'A broad category of quotes reflecting on the meaning, beauty, and challenges of life, offering perspectives that inspire and provoke thought.',
        '2025-01-01', false),
        ('b9f6c7d8-8d76-4b1f-9c87-e3c2c9d4f6e8', 'Resilience',
        'Quotes that encourage strength and perseverance in the face of adversity, reminding us of the power of hope and determination.',
        '2025-01-01', false),
        ('c0f7d8e9-9e87-4c2f-9d98-f4c3d0e5f7e9', 'Humor',
        N'Light-hearted and witty quotes that bring a smile to your face and offer a playful perspective on life’s quirks and challenges.',
        '2025-01-01', false),
        ('d1f8e9f0-a087-4d3f-9ea9-f5c4d1e6f8f0', 'Nature',
        'This category celebrates the beauty and wonder of the natural world, inspiring awe, mindfulness, and a sense of connection to the environment.',
        '2025-01-01', false),
        ('e2f9f0a1-b198-4e4f-9fab-f6c5d2e7f9a1', 'Leadership',
        'Quotes that inspire effective leadership, vision, and integrity, aimed at guiding individuals to inspire and influence others positively.',
        '2025-01-01', false),
        ('f3a0b2c3-c2a9-4f5f-9fbc-f7c6d3f8a0b2', 'Creativity',
        'Focused on imagination, innovation, and the creative process, these quotes inspire you to think outside the box and embrace your artistic side.',
        '2025-01-01', false);
        """;
}