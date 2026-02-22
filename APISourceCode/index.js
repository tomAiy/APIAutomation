"use strict";

const express = require("express");
const bodyParser = require("body-parser");

const app = express();
app.use(bodyParser.json());

// Initial data load
const books = require("./data/books");

// In‑memory DB clone
let booksDB = JSON.parse(JSON.stringify(books));

// HTTP status constants
const STATUS = {
    NOT_FOUND: 404,
    FORBIDDEN: 403,
    ACCEPTED: 202,
    NO_CONTENT: 204
};

// Utility functions
const getBookById = id =>
    booksDB.find(book => book.bookId === id);

const getBookIndex = id =>
    booksDB.findIndex(book => book.bookId === id);

const bookExists = id =>
    getBookIndex(id) !== -1;

const replaceBook = updatedBook => {
    booksDB = booksDB.filter(book => book.bookId !== updatedBook.bookId);
    booksDB.push(updatedBook);
};

const randomInt = (min, max) =>
    Math.floor(Math.random() * (max - min + 1)) + min;

const delayMs = seconds =>
    seconds * 1000;

const randomUpdateDelay = () =>
    delayMs(randomInt(2, 5));

const addBookAsync = book =>
    setTimeout(() => {
        booksDB.push(book);
    }, randomUpdateDelay());

const sendNotFound = res =>
    res.status(STATUS.NOT_FOUND).send();

// Routes
app.get("/books", (req, res) => {
    res.json(booksDB);
});

app.get("/book/:id", (req, res) => {
    const { id } = req.params;
    bookExists(id)
        ? res.json(getBookById(id))
        : sendNotFound(res);
});

app.put("/book", (req, res) => {
    const { bookId } = req.body;

    if (!bookExists(bookId)) {
        return sendNotFound(res);
    }

    replaceBook(req.body);
    res.status(STATUS.NO_CONTENT).send();
});

app.post("/book", (req, res) => {
    const { bookId } = req.body;

    if (bookExists(bookId)) {
        return res.status(STATUS.FORBIDDEN).send();
    }

    addBookAsync(req.body);
    res.status(STATUS.ACCEPTED).send();
});

app.delete("/book/:id", (req, res) => {
    const { id } = req.params;

    if (!bookExists(id)) {
        return sendNotFound(res);
    }

    booksDB = booksDB.filter(book => book.bookId !== id);
    res.status(STATUS.NO_CONTENT).send();
});

// Server start
app.listen(8080, () =>
    console.log("Books API running on port 8080")
);