package com.swaggerdemo.dto;


import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;

public class CustomerDto {
    @Schema(description = "Unique ID of the customer", example = "1")
    private Long id;

    @NotBlank
    @Schema(description = "Full name of the customer", example = "John Doe")
    private String name;

    @Email
    @Schema(description = "Email address", example = "john.doe@example.com")
    private String email;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public void setId(Long id)
    {
        this.id = id;
    }

    public Long getId()
    {
        return id;
    }
}
