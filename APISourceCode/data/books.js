'use strict';

const books = [
    {
        'bookId': '1',
        'bookData': {
            'title': 'The Iron Dominion',
            'fiction': true
        },
        'details': {
            'pages': 420,
            'format': 'hardcover',
            'illustrated': true,
            'specialEdition': false
        },
        'features': [
            {
                'name': 'Map Included',
                'extended': false
            },
            {
                'name': 'Author Notes',
                'extended': true
            },
            {
                'name': 'Bonus Chapter',
                'extended': true
            }
        ],
        'available': true,
        'onSale': true
    },
    {
        'bookId': '2',
        'bookData': {
            'title': 'Gardens of the North',
            'fiction': false
        },
        'details': {
            'pages': 300,
            'format': 'paperback',
            'illustrated': false,
            'specialEdition': false
        },
        'features': [
            {
                'name': 'Photography Section',
                'extended': false
            },
            {
                'name': 'Glossary',
                'extended': false
            },
            {
                'name': 'Index',
                'extended': false
            },
            {
                'name': 'Planting Guide',
                'extended': false
            }
        ],
        'available': true,
        'onSale': false
    },
    {
        'bookId': '3',
        'bookData': {
            'title': 'Minimalist Living',
            'fiction': false
        },
        'details': {
            'pages': 180,
            'format': 'ebook',
            'illustrated': false,
            'specialEdition': false
        },
        'features': [
            {
                'name': 'Workbook Pages',
                'extended': true
            },
            {
                'name': 'Audio Companion',
                'extended': true
            }
        ],
        'available': true,
        'onSale': false
    },
];

module.exports = books;