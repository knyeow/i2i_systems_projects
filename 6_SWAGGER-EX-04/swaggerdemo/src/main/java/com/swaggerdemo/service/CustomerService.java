package com.swaggerdemo.service;

import com.swaggerdemo.dto.CustomerDto;

import java.util.*;


public interface CustomerService {

    CustomerDto createCustomer(CustomerDto customerDto);
    CustomerDto getCustomerById(Long id);
    List<CustomerDto> getAllCustomers();
    CustomerDto updateCustomer(Long id, CustomerDto customerDto);
    void deleteCustomer(Long id);
}
