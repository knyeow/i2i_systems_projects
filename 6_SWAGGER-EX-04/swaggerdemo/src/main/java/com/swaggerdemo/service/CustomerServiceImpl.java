package com.swaggerdemo.service;


import com.swaggerdemo.dto.*;
import com.swaggerdemo.exception.*;
import org.springframework.stereotype.Service;

import java.util.*;

@Service
public class CustomerServiceImpl implements CustomerService
{ 
    private final Map<Long, CustomerDto> customerMap = new HashMap<>();
    private long idCounter = 1L;

   @Override
   public CustomerDto createCustomer(CustomerDto customerDto)
   {
        customerDto.setId(idCounter++); //??
        customerMap.put(customerDto.getId(), customerDto);
        return customerDto;
   } 

   @Override
   public CustomerDto getCustomerById(Long id)
   {
        CustomerDto customer = customerMap.get(id);
        if(customer == null)
        {
            throw new CustomerNotFoundException("Customer not found with ID: " + id);

        }

        return customer;
   }

   @Override
   public List<CustomerDto> getAllCustomers()
   {
        return new ArrayList<>(customerMap.values());
   }

   @Override
   public CustomerDto updateCustomer(Long id, CustomerDto customerDto)
   {
        if(!customerMap.containsKey(id))
        {
            throw new CustomerNotFoundException("Customer not found with ID: " + id);

        }
        customerDto.setId(id);
        customerMap.put(id, customerDto);

        return customerDto;
   }

   @Override
   public void deleteCustomer(Long id)
   {
        if(!customerMap.containsKey(id))
        {
            throw new CustomerNotFoundException("Customer not found with ID: " + id);

        }

        customerMap.remove(id);

   }
}
