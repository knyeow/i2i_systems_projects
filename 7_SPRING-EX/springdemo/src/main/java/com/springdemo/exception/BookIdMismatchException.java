package com.springdemo.exception;

public class BookIdMismatchException extends RuntimeException {
    public BookIdMismatchException() {
        super();
    }

    public BookIdMismatchException(String message) {
        super(message);
    }

    public BookIdMismatchException(String message, Throwable cause) {
        super(message, cause);
    }

    public BookIdMismatchException(Throwable cause) {
        super(cause);
    }
}