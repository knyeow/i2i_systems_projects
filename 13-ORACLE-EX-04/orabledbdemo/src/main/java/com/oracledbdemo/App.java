package com.oracledbdemo;

import java.sql.*;
import java.util.UUID;

public class App {

    public static void main(String[] args) {
        Connection con = null;

        try {
            Class.forName("oracle.jdbc.OracleDriver");
            System.out.println("Oracle JDBC driver loaded.");

            con = DriverManager.getConnection(
                    "*", 
                    "SYSTEM",
                    "ORACLE"
            );
            System.out.println("Database connected.");

            Statement stmt = con.createStatement();
            

            for (int i = 2; i <= 100; i++) {
                String name = "Book_" + i;
                String isbn = UUID.randomUUID().toString().substring(0, 13);

                String insertSql = "INSERT INTO SYSTEM.BOOK (ID, NAME, ISBN) VALUES (" +
                        i + ", '" + name + "', '" + isbn + "')";

                stmt.executeUpdate(insertSql);
            }
            System.out.println("100 kayıt başarıyla eklendi.");
            con.close();
        } catch (Exception e) {
            System.err.println("Exception: " + e.getMessage());
            e.printStackTrace();
        }
    }
}
