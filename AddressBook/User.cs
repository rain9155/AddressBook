using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    class User
    {
        private String name;
        private String birthday;
        private String sex;
        private String address;
        private String workAddress;
        private String phone;
        private String access;

        public User()
        {
        }

        public string Name {
            get => name;
            set => name = value == null ? "" : value;
        }

        public string Birthday {
            get => birthday;
            set => birthday =  value == null ? "" : value;
        }

        public string Sex
        {
            get => sex;
            set => sex = value == null ? "" : value;
        }

        public string Address
        {
            get => address;
            set => address = value == null ? "" : value;
        }

        public string WorkAddress
        {
            get => workAddress;
            set => workAddress = value == null ? "" : value;
        }

        public string Phone
        {
            get => phone;
            set => phone = value == null ? "" : value;
        }

        public string Access {
            get => access;
            set => access = value == null ? "" : value;
        }
    }
}
