package com.springdemo.config;

import org.springframework.context.annotation.*;
import org.springframework.security.config.annotation.web.builders.*;
import org.springframework.security.config.annotation.web.configuration.*;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.web.SecurityFilterChain;

@Configuration
@EnableWebSecurity
public class SecurityConfig {

    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        http
          .authorizeHttpRequests(expressionInterceptUrlRegistry ->
            expressionInterceptUrlRegistry
              .anyRequest()
              .permitAll())
          .csrf(AbstractHttpConfigurer::disable);
        return http.build();
    }
}