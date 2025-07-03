package com.kafkacommunication;

class Student {
  // data member (also instance variable)
  int id;
  // data member (also instance variable)
  String name;
  public Student(int id, String name) {
    this.id = id;
    this.name = name;
  }


    @Override
    public String toString() {
        return "Student{id=" + id + ", name='" + name + "'}";
    }
}
