﻿using Alan_WarrenDesafio1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alan_WarrenDesafio1.Data
{
    public class CustomerServices : ICustomerServices
    {
        private IList<Customer> Customers { get; set; } = new List<Customer>();

        public IEnumerable<Customer> GetAll(Func<Customer, bool> predicate = null)
        {
            if (predicate is null)
            {
                return Customers;
            }

            var customer = Customers.Where(predicate).ToList();

            return customer.Count is 0
                ? null
                : customer;
        }

        public Customer GetBy(Func<Customer, bool> predicate)
        {
            var customer = Customers.FirstOrDefault(predicate);

            return customer;
        }

        public bool Create(Customer newCustomer)
        {
            if (CustomerExists(newCustomer, Customers)) return false;

            int autoIncrementId = Customers.LastOrDefault()?.Id ?? default;

            newCustomer.Id = autoIncrementId + 1;
            Customers.Add(newCustomer);
            return true;
        }

        public int Update(int id, Customer newCustomer)
        {
            var customerToUpdate = GetBy(x => x.Id == id);
            if (customerToUpdate is null) return 1;

            if (CustomerExists(newCustomer, Customers)) return 2;

            customerToUpdate.FullName = newCustomer.FullName;
            customerToUpdate.Email = newCustomer.Email;
            customerToUpdate.EmailConfirmation = newCustomer.EmailConfirmation;
            customerToUpdate.Cpf = newCustomer.Cpf;
            customerToUpdate.Cellphone = newCustomer.Cellphone;
            customerToUpdate.EmailSms = newCustomer.EmailSms;
            customerToUpdate.Whatsapp = newCustomer.Whatsapp;
            customerToUpdate.Country = newCustomer.Country;
            customerToUpdate.City = newCustomer.City;
            customerToUpdate.PostalCode = newCustomer.PostalCode;
            customerToUpdate.Adress = newCustomer.Adress;
            customerToUpdate.Number = newCustomer.Number;

            return 0;
        }

        public bool Delete(int id)
        {
            var customerToDelete = GetBy(x => x.Id == id);
            return customerToDelete is not null
            && Customers.Remove(customerToDelete);
        }

        private static bool CustomerExists(Customer newCustomer, IList<Customer> customers)
        {
            return customers.Any(x => x.Email == newCustomer.Email || x.Cpf == newCustomer.Cpf);
        }
    }
}