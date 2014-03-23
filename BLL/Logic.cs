using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Logic
    {
        public List<UserVM> GetAllUsers()
        {
            List<UserVM> userVmList = new List<UserVM>();
            List<UserDM> userDmList = new List<UserDM>();
            UserDAO dao = new UserDAO();
            userDmList = dao.GetAllUsers();
            foreach (UserDM userDm in userDmList)
            {
                userVmList.Add(ConvertUserDmToVm(userDm));
            }
            return userVmList;
        }
        public UserVM ConvertUserDmToVm(UserDM user)
        {
            UserVM userVm = new UserVM();
            if (user != null)
            {
                userVm.id = user.id;
                userVm.lastName = GetPerson(GetPersonAddress(user.personAddressId).personId).lastName;
                userVm.firstName = GetPerson(GetPersonAddress(user.personAddressId).personId).firstName;
                userVm.email = GetPerson(GetPersonAddress(user.personAddressId).personId).email;
                userVm.dateOfBirth = GetPerson(GetPersonAddress(user.personAddressId).personId).dateOfBirth;
                userVm.street = GetAddress(GetPersonAddress(user.personAddressId).addressId).street;
                userVm.city = GetAddress(GetPersonAddress(user.personAddressId).addressId).city;
                userVm.state = GetAddress(GetPersonAddress(user.personAddressId).addressId).state;
                userVm.zip = GetAddress(GetPersonAddress(user.personAddressId).addressId).zip;
                userVm.addressType = GetType(GetPersonAddress(user.personAddressId).typeId);
                userVm.number = GetPhone(GetPersonPhone(user.personPhoneId).phoneId);
                userVm.phoneType = GetType(GetPersonPhone(user.personPhoneId).typeId);
            }
            return userVm;
        }
        public UserDM ConvertUserVmToDm(UserVM userVm)
        {
            UserDM userDm = new UserDM();
            if (userVm != null)
            {
                SetPersonAddressId(userVm, userDm);
                SetPersonPhoneId(userVm, userDm);
            }
            return userDm;
        }
        public int SetTypeId(string type)
        {
            int x = 0;
            bool z = DoesTypeExist(GetTypeId(type));
            if (z == false)
            {
                CreateType(type);
                x = GetTypeId(type);
            }
            else
            {
                x = GetTypeId(type);
            }
            return x;
        }
        public TypeDM CreateType(string type)
        {
            TypeDM typeDm = new TypeDM();
            TypeDAO dao = new TypeDAO();
            typeDm.type = type;
            dao.CreateType(type);
            return typeDm;
        }
        public CategoryDM CreateCategory(string category)
        {
            CategoryDM categoryDm = new CategoryDM();
            CategoryDAO dao = new CategoryDAO();
            categoryDm.category = category;
            dao.CreateCategory(category);
            return categoryDm;
        }
        public bool DoesCategoryExist(int id)
        {
            CategoryDAO dao = new CategoryDAO();
            return dao.DoesCategoryExist(id);
        }
        public int GetTypeId(string type)
        {
            TypeDAO dao = new TypeDAO();
            return dao.GetTypeId(type);
        }
        public bool DoesTypeExist(int id)
        {
            TypeDAO dao = new TypeDAO();
            return dao.DoesTypeExist(id);
        }
        public int SetAddressId(string street, string city, string state, string zip)
        {
            int x = 0;
            bool y = DoesAddressExist(GetAddressId(street, city, state, zip));
            if (y == false)
            {
                CreateAddress(street, city, state, zip);
                x = GetAddressId(street, city, state, zip);
            }
            else
            {
                x = GetAddressId(street, city, state, zip);
            }
            return x;
        }
        public int SetCategoryId(string category)
        {
            int x = 0;
            bool y = DoesCategoryExist(GetCategoryId(category));
            if (y == false)
            {
                CreateCategory(category);
                x = GetCategoryId(category);
            }
            else
            {
                x = GetCategoryId(category);
            }
            return x;
        }
        public AddressDM CreateAddress(string street, string city, string state, string zip)
        {
            AddressDM address = new AddressDM();
            AddressDAO dao = new AddressDAO();
            address.street = street;
            address.city = city;
            address.state = state;
            address.zip = zip;
            dao.CreateAddress(street, city, state, zip);
            return address;
        }
        public int GetAddressId(string street, string city, string state, string zip)
        {
            AddressDAO dao = new AddressDAO();
            return dao.GetAddressId(street, city, state, zip);
        }
        public int GetCategoryId(string category)
        {
            CategoryDAO dao = new CategoryDAO();
            return dao.GetCategoryId(category);
        }
        public bool DoesAddressExist(int id)
        {
            AddressDAO dao = new AddressDAO();
            return dao.DoesAddressExist(id);
        }
        public PersonAddressDM CreatePersonAddress(string lastName, string firstName, string email, DateTime dateOfBirth,
            string street, string city, string state, string zip, string type)
        {
            PersonAddressDAO dao = new PersonAddressDAO();
            PersonAddressDM personAddress = new PersonAddressDM();
            SetPersonAddressDmPersonId(lastName, firstName, email, dateOfBirth, personAddress);
            SetPersonAddressDmAddressId(street, city, state, zip, personAddress);
            SetPersonAddressDmTypeId(type, personAddress);
            dao.CreatePersonAddress(personAddress.personId, personAddress.addressId, personAddress.typeId);
            return personAddress;
        }
        public void SetPersonAddressDmTypeId(string type, PersonAddressDM personAddress)
        {
            bool z = DoesTypeExist(SetTypeId(type));
            if (z == false)
            {
                CreateType(type);
                personAddress.typeId = SetTypeId(type);
            }
            else
            {
                personAddress.typeId = SetTypeId(type);
            }
        }
        public void SetPersonAddressDmPersonId(string lastName, string firstName, string email,
            DateTime dateOfBirth, PersonAddressDM personAddress)
        {
            bool x = DoesPersonExist(SetPersonId(lastName, firstName, email, dateOfBirth));
            if (x == false)
            {
                CreatePerson(lastName, firstName, email, dateOfBirth);
                personAddress.personId = SetPersonId(lastName, firstName, email, dateOfBirth);
            }
            else
            {
                personAddress.personId = SetPersonId(lastName, firstName, email, dateOfBirth);
            }
        }
        public void SetPersonAddressDmAddressId(string street, string city, string state, string zip, PersonAddressDM personAddress)
        {
            bool y = DoesAddressExist(SetAddressId(street, city, state, zip));
            if (y == false)
            {
                CreateAddress(street, city, state, zip);
                personAddress.addressId = SetAddressId(street, city, state, zip);
            }
            else
            {
                personAddress.addressId = SetAddressId(street, city, state, zip);
            }
        }
        public bool DoesPersonAddressExist(int id)
        {
            PersonAddressDAO dao = new PersonAddressDAO();
            return dao.DoesPersonAddressExist(id);
        }
        public void SetPersonPhoneId(UserVM userVm, UserDM userDm)
        {
            bool w = DoesPersonPhoneExist(GetPersonPhoneId(SetPersonId(userVm.lastName, userVm.firstName, userVm.email,
                userVm.dateOfBirth), SetPhoneId(userVm.number), SetTypeId(userVm.phoneType)));
            if (w == false)
            {
                PersonPhoneDM phone = CreatePersonPhone(userVm.lastName, userVm.firstName, userVm.email, userVm.dateOfBirth,
                    userVm.number, userVm.phoneType);
                userDm.personPhoneId = GetPersonPhoneId(phone.personId, phone.phoneId, phone.typeId);
            }
            else
            {
                userDm.personPhoneId = GetPersonPhoneId(SetPersonId(userVm.lastName, userVm.firstName,
                    userVm.email, userVm.dateOfBirth), SetPhoneId(userVm.number), SetTypeId(userVm.phoneType));
            }
        }
        public PersonPhoneDM CreatePersonPhone(string lastName, string firstName, string email, DateTime dateOfBirth,
            string number, string type)
        {
            PersonPhoneDAO dao = new PersonPhoneDAO();
            PersonPhoneDM personPhone = new PersonPhoneDM();
            SetPersonPhoneDmPersonId(lastName, firstName, email, dateOfBirth, personPhone);
            SetPersonPhoneDmPhoneId(number, personPhone);
            SetPersonPhoneDmTypeId(type, personPhone);
            dao.CreatePersonPhone(personPhone.personId, personPhone.phoneId, personPhone.typeId);
            return personPhone;
        }
        public void SetPersonPhoneDmPersonId(string lastName, string firstName, string email, DateTime dateOfBirth,
            PersonPhoneDM personPhone)
        {
            bool x = DoesPersonExist(SetPersonId(lastName, firstName, email, dateOfBirth));
            if (x == false)
            {
                CreatePerson(lastName, firstName, email, dateOfBirth);
                personPhone.personId = SetPersonId(lastName, firstName, email, dateOfBirth);
            }
            else
            {
                personPhone.personId = SetPersonId(lastName, firstName, email, dateOfBirth);
            }
        }
        public void UpdateUser(int id, string lastName, string firstName, string email, DateTime dateOfBirth,
            string street, string city, string state, string zip, string addressType,
            string number, string phoneType)
        {
            UserDM user = new UserDM();
            UserDAO dao = new UserDAO();
            PersonAddressDAO padao = new PersonAddressDAO();
            PersonPhoneDAO ppdao = new PersonPhoneDAO();
            user.personAddressId = SetUserDmPersonAddressId(lastName, firstName, email, dateOfBirth,
                street, city, state, zip, addressType, user, padao);
            user.personPhoneId = SetUserDmPersonPhoneId(lastName, firstName, email, dateOfBirth, number, phoneType, user, ppdao);
            padao.UpdatePersonAddressDB(id, lastName, firstName, email, dateOfBirth, street, city, state, zip, addressType);
            ppdao.UpdatePersonPhoneDB(id, lastName, firstName, email, dateOfBirth, number, phoneType);
            dao.UpdateUserDB(id, user.personAddressId, user.personPhoneId);
        }
        public int SetUserDmPersonPhoneId(string lastName, string firstName, string email, DateTime dateOfBirth,
            string number, string phoneType, UserDM user, PersonPhoneDAO ppdao)
        {
            int a = SetPersonId(lastName, firstName, email, dateOfBirth);
            int b = SetPhoneId(number);
            int c = SetTypeId(phoneType);
            bool x = ppdao.DoesPersonPhoneExist(GetPersonPhoneId(a, b, c));
            if (x == false)
            {
                ppdao.CreatePersonPhone(a, b, c);
                user.personPhoneId = GetPersonPhoneId(a, b, c);
            }
            else
            {
                user.personPhoneId = GetPersonPhoneId(a, b, c);
            }
            return user.personPhoneId;
        }
        public int SetUserDmPersonPhoneId(OrderVM orderVm, UserDM userDm)
        {
            bool w = DoesPersonPhoneExist(GetPersonPhoneId(SetPersonId(orderVm.lastName, orderVm.firstName, orderVm.email,
                orderVm.dateOfBirth), SetPhoneId(orderVm.number), SetTypeId(orderVm.phoneType)));
            if (w == false)
            {
                PersonPhoneDM phone = CreatePersonPhone(orderVm.lastName, orderVm.firstName, orderVm.email, orderVm.dateOfBirth,
                    orderVm.number, orderVm.phoneType);
                userDm.personPhoneId = GetPersonPhoneId(phone.personId, phone.phoneId, phone.typeId);
            }
            else
            {
                userDm.personPhoneId = GetPersonPhoneId(SetPersonId(orderVm.lastName, orderVm.firstName, orderVm.email,
                    orderVm.dateOfBirth), SetPhoneId(orderVm.number), SetTypeId(orderVm.phoneType));
            }
            return userDm.personPhoneId;
        }
        public int SetPersonAddressId(UserVM userVm, UserDM userDm)
        {
            bool w = DoesPersonAddressExist(GetPersonAddressId(SetPersonId(userVm.lastName, userVm.firstName, userVm.email, 
                userVm.dateOfBirth), SetAddressId(userVm.street, userVm.city, userVm.state, userVm.zip),
                SetTypeId(userVm.addressType)));
            if (w == false)
            {
                PersonAddressDM address = CreatePersonAddress(userVm.lastName, userVm.firstName, userVm.email, userVm.dateOfBirth,
                    userVm.street, userVm.city, userVm.state, userVm.zip, userVm.addressType);
                userDm.personAddressId = GetPersonAddressId(address.personId, address.addressId, address.typeId);
            }
            else
            {
                userDm.personAddressId = GetPersonAddressId(SetPersonId(userVm.lastName, userVm.firstName, userVm.email,
                    userVm.dateOfBirth), SetAddressId(userVm.street, userVm.city, userVm.state, userVm.zip),
                    SetTypeId(userVm.addressType));
            }
            return userDm.personAddressId;
        }
        public int SetUserDmPersonAddressId(OrderVM orderVm, UserDM userDm)
        {
            bool w = DoesPersonAddressExist(GetPersonAddressId(SetPersonId(orderVm.lastName, orderVm.firstName, orderVm.email,
                orderVm.dateOfBirth), SetAddressId(orderVm.street, orderVm.city, orderVm.state, orderVm.zip),
                SetTypeId(orderVm.addressType)));
            if (w == false)
            {
                PersonAddressDM address = CreatePersonAddress(orderVm.lastName, orderVm.firstName, orderVm.email,
                    orderVm.dateOfBirth, orderVm.street, orderVm.city, orderVm.state, orderVm.zip, orderVm.addressType);
                userDm.personAddressId = GetPersonAddressId(address.personId, address.addressId, address.typeId);
            }
            else
            {
                userDm.personAddressId = GetPersonAddressId(SetPersonId(orderVm.lastName, orderVm.firstName, orderVm.email,
                    orderVm.dateOfBirth), SetAddressId(orderVm.street, orderVm.city, orderVm.state, orderVm.zip),
                    SetTypeId(orderVm.addressType));
            }
            return userDm.personAddressId;
        }
        public void DeleteUser(int id)
        {
            UserDAO dao = new UserDAO();
            dao.DeleteUser(id);
        }
        public int SetUserDmPersonAddressId(string lastName, string firstName, string email, DateTime dateOfBirth,
            string street, string city, string state, string zip, string addressType,
            UserDM user, PersonAddressDAO padao)
        {
            int a = SetPersonId(lastName, firstName, email, dateOfBirth);
            int b = SetAddressId(street, city, state, zip);
            int c = SetTypeId(addressType);
            bool x = padao.DoesPersonAddressExist(GetPersonAddressId(a, b, c));
            if (x == false)
            {
                padao.CreatePersonAddress(a, b, c);
                user.personAddressId = GetPersonAddressId(a, b, c);
            }
            else
            {
                user.personAddressId = GetPersonAddressId(a, b, c);
            }
            return user.personAddressId;
        }
        public void SetPersonPhoneDmPhoneId(string number, PersonPhoneDM personPhone)
        {
            bool y = DoesPhoneExist(SetPhoneId(number));
            if (y == false)
            {
                CreatePhone(number);
                personPhone.phoneId = SetPhoneId(number);
            }
            else
            {
                personPhone.phoneId = SetPhoneId(number);
            }
        }
        public void SetPersonPhoneDmTypeId(string type, PersonPhoneDM personPhone)
        {
            bool z = DoesTypeExist(SetTypeId(type));
            if (z == false)
            {
                CreateType(type);
                personPhone.typeId = SetTypeId(type);
            }
            else
            {
                personPhone.typeId = SetTypeId(type);
            }
        }
        public bool DoesPhoneExist(int id)
        {
            PhoneDAO dao = new PhoneDAO();
            return dao.DoesPhoneExist(id);
        }
        public int GetPhoneId(string number)
        {
            PhoneDAO dao = new PhoneDAO();
            return dao.GetPhoneId(number);
        }
        public int SetPhoneId(string number)
        {
            int y = 0;
            bool x = DoesPhoneExist(GetPhoneId(number));
            if (x == false)
            {
                CreatePhone(number);
                y = GetPhoneId(number);
            }
            else
            {
                y = GetPhoneId(number);
            }
            return y;
        }
        public PhoneDM CreatePhone(string number)
        {
            PhoneDM phone = new PhoneDM();
            PhoneDAO dao = new PhoneDAO();
            phone.number = number;
            dao.CreatePhone(number);
            return phone;
        }
        public int SetPersonId(string lastName, string firstName, string email, DateTime dateOfBirth)
        {
            int y = 0;
            bool x = DoesPersonExist(GetPersonId(lastName, firstName, email, dateOfBirth));
            if (x == false)
            {
                CreatePerson(lastName, firstName, email, dateOfBirth);
                y = GetPersonId(lastName, firstName, email, dateOfBirth);
            }
            else
            {
                y = GetPersonId(lastName, firstName, email, dateOfBirth);
            }
            return y;
        }
        public static bool DoesPersonExist(UserVM userVm, PersonDAO personDao)
        {
            bool a = personDao.DoesPersonExist(personDao.GetPersonId(userVm.lastName, userVm.firstName, userVm.email,
                userVm.dateOfBirth));
            return a;
        }
        public bool DoesPersonExist(int id)
        {
            PersonDAO dao = new PersonDAO();
            return dao.DoesPersonExist(id);
        }
        public PersonDM CreatePerson(string lastName, string firstName, string email, DateTime dateOfBirth)
        {
            PersonDM person = new PersonDM();
            person.lastName = lastName;
            person.firstName = firstName;
            person.email = email;
            person.dateOfBirth = dateOfBirth;
            PersonDAO dao = new PersonDAO();
            dao.CreatePerson(lastName, firstName, email, dateOfBirth);
            return person;
        }
        public int GetPersonAddressId(int personId, int addressId, int typeId)
        {
            PersonAddressDAO dao = new PersonAddressDAO();
            return dao.GetPersonAddressId(personId, addressId, typeId);
        }
        public int GetPersonPhoneId(int personId, int phoneId, int typeId)
        {
            PersonPhoneDAO dao = new PersonPhoneDAO();
            return dao.GetPersonPhoneId(personId, phoneId, typeId);
        }
        public int GetPersonId(string lastName, string firstName, string email, DateTime dateOfBirth)
        {
            PersonDAO dao = new PersonDAO();
            return dao.GetPersonId(lastName, firstName, email, dateOfBirth);
        }
        public bool DoesPersonPhoneExist(int id)
        {
            PersonPhoneDAO dao = new PersonPhoneDAO();
            return dao.DoesPersonPhoneExist(id);
        }
        public AddressDM GetAddress(int id)
        {
            AddressDAO dao = new AddressDAO();
            return dao.GetAddress(id);
        }
        public PersonDM GetPerson(int id)
        {
            PersonDAO dao = new PersonDAO();
            return dao.GetPerson(id);
        }
        public PersonPhoneDM GetPersonPhone(int id)
        {
            PersonPhoneDAO dao = new PersonPhoneDAO();
            return dao.GetPersonPhone(id);
        }
        public PersonAddressDM GetPersonAddress(int id)
        {
            PersonAddressDAO dao = new PersonAddressDAO();
            return dao.GetPersonAddress(id);
        }
        public string GetType(int id)
        {
            TypeDAO dao = new TypeDAO();
            return dao.GetType(id);
        }
        public string GetPhone(int id)
        {
            PhoneDAO dao = new PhoneDAO();
            return dao.GetPhone(id);
        }
        public void CreateUser(int personAddressId, int personPhoneId)
        {
            UserDAO dao = new UserDAO();
            dao.CreateUser(personAddressId, personPhoneId);
        }
        public UserVM CreateUserVM(int id, string lastName, string firstName, string email, DateTime dateOfBirth,
            string street, string city, string state, string zip, string addressType,
            string number, string phoneType)
        {
            UserVM user = new UserVM();
            user.id = id;
            user.lastName = lastName;
            user.firstName = firstName;
            user.email = email;
            user.dateOfBirth = dateOfBirth;
            user.street = street;
            user.city = city;
            user.state = state;
            user.zip = zip;
            user.addressType = addressType;
            user.number = number;
            user.phoneType = phoneType;
            UserDM userDm = ConvertUserVmToDm(user);
            CreateUser(userDm.personAddressId, userDm.personPhoneId);
            return user;
        }
        public List<EquipmentVM> GetAllEquipment()
        {
            List<EquipmentVM> equipmentVmList = new List<EquipmentVM>();
            List<EquipmentDM> equipmentDmList = new List<EquipmentDM>();
            EquipmentDAO dao = new EquipmentDAO();
            equipmentDmList = dao.GetAllEquipment();
            foreach (EquipmentDM equipmentDm in equipmentDmList)
            {
                equipmentVmList.Add(ConvertEquipmentDmToVm(equipmentDm));
            }
            return equipmentVmList;
        }
        public EquipmentVM ConvertEquipmentDmToVm(EquipmentDM equipment)
        {
            EquipmentVM equipmentVm = new EquipmentVM();
            if (equipment != null)
            {
                CategoryDAO dao = new CategoryDAO();
                TypeDAO tdao = new TypeDAO();
                equipmentVm.id = equipment.id;
                equipmentVm.category = dao.GetCategory(equipment.categoryId).ToString();
                equipmentVm.type = tdao.GetType(equipment.typeId).ToString();
            }
            return equipmentVm;
        }
        public EquipmentVM CreateEquipmentVM(string category, string type)
        {
            EquipmentDAO dao = new EquipmentDAO();
            EquipmentVM equipment = new EquipmentVM();
            equipment.category = category;
            equipment.type = type;
            EquipmentDM equipmentDm = ConvertEquipmentVmToDm(equipment);
            dao.CreateEquipment(equipmentDm.categoryId, equipmentDm.typeId);
            return equipment;
        }
        public EquipmentDM ConvertEquipmentVmToDm(EquipmentVM equipmentVm)
        {
            EquipmentDM equipmentDm = new EquipmentDM();
            if (equipmentVm != null)
            {
                CategoryDAO dao = new CategoryDAO();
                TypeDAO tdao = new TypeDAO();
                equipmentDm.categoryId = dao.GetCategoryId(equipmentVm.category);
                equipmentDm.typeId = tdao.GetTypeId(equipmentVm.type);
                bool x = dao.DoesCategoryExist(dao.GetCategoryId(equipmentVm.category));
                if (x == false)
                {
                    dao.CreateCategory(equipmentVm.category);
                    equipmentDm.categoryId = dao.GetCategoryId(equipmentVm.category);
                }
                else
                {
                    equipmentDm.categoryId = dao.GetCategoryId(equipmentVm.category);
                }
                bool y = tdao.DoesTypeExist(tdao.GetTypeId(equipmentVm.type));
                if (x == false)
                {
                    tdao.CreateType(equipmentVm.type);
                    equipmentDm.typeId = tdao.GetTypeId(equipmentVm.type);
                }
                else
                {
                    equipmentDm.typeId = tdao.GetTypeId(equipmentVm.type);
                }
            }
            return equipmentDm;
        }
        public void UpdateEquipment(int id, string category, string type)
        {
            EquipmentDM equipmentDm = new EquipmentDM();
            CategoryDAO cdao = new CategoryDAO();
            TypeDAO tdao = new TypeDAO();
            EquipmentDAO edao = new EquipmentDAO();
            equipmentDm.categoryId = SetCategoryId(category);
            equipmentDm.typeId = SetTypeId(type);
            edao.UpdateEquipmentDB(id, equipmentDm.categoryId, equipmentDm.typeId);
        }
        public void DeleteEquipment(int id)
        {
            EquipmentDAO dao = new EquipmentDAO();
            dao.DeleteEquipment(id);
        }
        public List<OrderVM> GetAllOrders()
        {
            List<OrderVM> orderVmList = new List<OrderVM>();
            List<OrderDM> orderDmList = new List<OrderDM>();
            OrderDAO dao = new OrderDAO();
            orderDmList = dao.GetAllOrders();
            foreach (OrderDM orderDm in orderDmList)
            {
                orderVmList.Add(ConvertOrderDmToVm(orderDm));
            }
            return orderVmList;
        }
        public OrderVM ConvertOrderDmToVm(OrderDM order)
        {
            OrderVM orderVm = new OrderVM();
            if (order != null)
            {
                orderVm.id = order.id;
                orderVm.category = GetCategory(GetEquipment(order.equipmentId).categoryId);
                orderVm.type = GetType(GetEquipment(order.equipmentId).typeId);
                orderVm.lastName = GetPerson(GetPersonAddress(GetUser(order.userId).personAddressId).personId).lastName;
                orderVm.firstName = GetPerson(GetPersonAddress(GetUser(order.userId).personAddressId).personId).firstName;
                orderVm.email = GetPerson(GetPersonAddress(GetUser(order.userId).personAddressId).personId).email;
                orderVm.dateOfBirth = GetPerson(GetPersonAddress(GetUser(order.userId).personAddressId).personId).dateOfBirth;
                orderVm.street = GetAddress(GetPersonAddress(GetUser(order.userId).personAddressId).addressId).street;
                orderVm.city = GetAddress(GetPersonAddress(GetUser(order.userId).personAddressId).addressId).city;
                orderVm.state = GetAddress(GetPersonAddress(GetUser(order.userId).personAddressId).addressId).state;
                orderVm.zip = GetAddress(GetPersonAddress(GetUser(order.userId).personAddressId).addressId).zip;
                orderVm.addressType = GetType(GetPersonAddress(GetUser(order.userId).personAddressId).typeId);
                orderVm.number = GetPhone(GetPersonPhone(GetUser(order.userId).personPhoneId).phoneId);
                orderVm.phoneType = GetType(GetPersonPhone(GetUser(order.userId).personPhoneId).typeId);
            }
            return orderVm;
        }
        public UserDM GetUser(int id)
        {
            UserDAO dao = new UserDAO();
            return dao.GetUserById(id);
        }
        public string GetCategory(int id)
        {
            CategoryDAO dao = new CategoryDAO();
            return dao.GetCategory(id);
        }
        public EquipmentDM GetEquipment(int id)
        {
            EquipmentDAO dao = new EquipmentDAO();
            return dao.GetEquipmentById(id);
        }
        public OrderVM CreateOrder(string category, string type, string lastName, string firstName, string email,
            DateTime dateOfBirth, string street, string city, string state, string zip, string addressType,
            string number, string phoneType)
        {
            OrderVM order = new OrderVM();
            order.category = category;
            order.type = type;
            order.lastName = lastName;
            order.firstName = firstName;
            order.email = email;
            order.dateOfBirth = dateOfBirth;
            order.street = street;
            order.city = city;
            order.state = state;
            order.zip = zip;
            order.addressType = addressType;
            order.number = number;
            order.phoneType = phoneType;
            OrderDM orderDm = ConvertOrderVmToDm(order);
            CreateOrderDB(orderDm);
            return order;
        }
        public OrderDM ConvertOrderVmToDm(OrderVM orderVm)
        {
            OrderDM orderDm = new OrderDM();
            if (orderVm != null)
            {
                orderDm.equipmentId = SetEquipmentId(orderVm, orderDm);
                orderDm.userId = SetUserId(orderVm, orderDm);
            }
            return orderDm;
        }
        public void CreateOrderDB(OrderDM order)
        {
            OrderDAO dao = new OrderDAO();
            dao.CreateOrder(order);
        }
        public int SetUserId(OrderVM orderVm, OrderDM orderDm)
        {
            UserDM user = new UserDM();
            bool x = DoesUserExist(GetUserId(SetUserDmPersonAddressId(orderVm, user),
                SetUserDmPersonPhoneId(orderVm, user)));
            if (x == false)
            {
                CreateUser(SetUserDmPersonAddressId(orderVm, user),
                    SetUserDmPersonPhoneId(orderVm, user));
                orderDm.userId = GetUserId(SetUserDmPersonAddressId(orderVm, user),
                    SetUserDmPersonPhoneId(orderVm, user));
            }
            else
            {
                orderDm.userId = GetUserId(SetUserDmPersonAddressId(orderVm, user),
                    SetUserDmPersonPhoneId(orderVm, user));
            }
            return orderDm.userId;
        }
        public int SetEquipmentId(OrderVM orderVm, OrderDM orderDm)
        {
            EquipmentDM equipment = new EquipmentDM();
            bool x = DoesEquipmentExist(GetEquipmentId(SetEquipmentDmCategoryId(orderVm, equipment),
                SetEquipmentDmTypeId(orderVm, equipment)));
            if (x == false)
            {
                CreateEquipment(SetEquipmentDmCategoryId(orderVm, equipment), SetEquipmentDmTypeId(orderVm, equipment));
                orderDm.equipmentId = GetEquipmentId(SetEquipmentDmCategoryId(orderVm, equipment),
                    SetEquipmentDmTypeId(orderVm, equipment));
            }
            else
            {
                orderDm.equipmentId = GetEquipmentId(SetEquipmentDmCategoryId(orderVm, equipment),
                    SetEquipmentDmTypeId(orderVm, equipment));
            }
            return orderDm.equipmentId;
        }
        public int SetEquipmentDmTypeId(OrderVM orderVm, EquipmentDM equipment)
        {
            bool x = DoesTypeExist(SetTypeId(orderVm.type));
            if (x == false)
            {
                CreateType(orderVm.type);
                equipment.typeId = SetTypeId(orderVm.type);
            }
            else
            {
                equipment.typeId = SetTypeId(orderVm.type);
            }
            return equipment.typeId;
        }
        public int SetEquipmentDmCategoryId(OrderVM orderVm, EquipmentDM equipment)
        {
            bool x = DoesCategoryExist(SetCategoryId(orderVm.category));
            if (x == false)
            {
                CreateCategory(orderVm.category);
                equipment.categoryId = SetCategoryId(orderVm.category);
            }
            else
            {
                equipment.categoryId = SetCategoryId(orderVm.category);
            }
            return equipment.categoryId;
        }
        public bool DoesUserExist(int userId)
        {
            UserDAO dao = new UserDAO();
            return dao.DoesUserExist(userId);
        }
        public bool DoesEquipmentExist(int equipmentId)
        {
            EquipmentDAO dao = new EquipmentDAO();
            return dao.DoesEquipmentExist(equipmentId);
        }
        public void CreateEquipment(int categoryId, int typeId)
        {
            EquipmentDAO dao = new EquipmentDAO();
            dao.CreateEquipment(categoryId, typeId);
        }
        public int GetEquipmentId(int categoryId, int typeId)
        {
            EquipmentDAO dao = new EquipmentDAO();
            EquipmentDM equipment = new EquipmentDM();
            equipment = dao.GetEquipment(categoryId, typeId);
            if (equipment == null)
            {
                CreateEquipment(categoryId, typeId);
                equipment = dao.GetEquipment(categoryId, typeId);
                //equipment.categoryId = dao.GetEquipment(categoryId).id;
                //equipment.typeId = dao.GetEquipment(typeId).typeId;
            }
            return equipment.id;
        }
        public int GetUserId(int personAddressId, int personPhoneId)
        {
            UserDAO dao = new UserDAO();
            UserDM user = new UserDM();
            int id = dao.GetUser(personAddressId, personPhoneId).id;
            if (id == 0)
            {
                CreateUser(personAddressId, personPhoneId);
                user = dao.GetUser(personAddressId, personPhoneId);
                //user.personAddressId = dao.GetUser(personAddressId, personPhoneId).id;
                //user.personPhoneId = dao.GetUser(personAddressId, personPhoneId).id;
            }
            return user.id;
        }
        public void UpdateOrder(int id, string category, string type, string lastName, string firstName, string email,
            DateTime dateOfBirth, string street, string city, string state, string zip, string addressType,
            string number, string phoneType)
        {
            OrderVM orderVm = new OrderVM();
            OrderDM order = new OrderDM();
            OrderDAO odao = new OrderDAO();
            UserDM user = new UserDM();
            UserDAO udao = new UserDAO(); 
            order.equipmentId = SetEquipmentId(category, type);
            order.userId = SetUserId(lastName, firstName, email, dateOfBirth, street, city, state, zip, addressType,
                number, phoneType);
            odao.UpdateOrderDB(id, order.equipmentId, order.userId);
        }
        public int SetEquipmentId(string category, string type)
        {
            EquipmentDAO dao = new EquipmentDAO();
            EquipmentDM equipment = new EquipmentDM();
            CategoryDAO cdao = new CategoryDAO();
            TypeDAO tdao = new TypeDAO();
            int a = SetCategoryId(category);
            int b = SetTypeId(type);
            int c = SetCatAndTypeId(a, b);
            bool x = dao.DoesEquipmentExist(c);
            if (x == false)
            {
                dao.CreateEquipment(a, b);
                equipment.categoryId = c;
                equipment.typeId = c;
            }
            else
            {
                equipment.categoryId = c;
                equipment.typeId = c;
            }
            return equipment.id;
        }
        public int SetUserId(string lastName, string firstName, string email, DateTime dateOfBirth,
            string street, string city, string state, string zip, string addressType,
            string number, string phoneType)
        {
            UserDM user = new UserDM();
            UserDAO udao = new UserDAO();
            PersonAddressDAO adao = new PersonAddressDAO();
            PersonPhoneDAO pdao = new PersonPhoneDAO();
            int a = SetPersonId(lastName, firstName, email, dateOfBirth);
            int b = SetAddressId(street, city, state, zip);
            int c = SetTypeId(addressType);
            int d = SetPhoneId(number);
            int e = SetTypeId(phoneType);
            int f = SetPersonAddressId(a, b, c);
            int g = SetPersonPhoneId(a, d, e);
            bool x = adao.DoesPersonAddressExist(f);
            if (x == false)
            {
                adao.CreatePersonAddress(a, b, c);
                user.personAddressId = f;
            }
            else
            {
                user.personAddressId = f;
            }
            bool y = pdao.DoesPersonPhoneExist(g);
            if (y == false)
            {
                pdao.CreatePersonPhone(a, d, e);
                user.personPhoneId = g;
            }
            else
            {
                user.personPhoneId = g;
            }
            bool z = udao.DoesUserExist(GetUserId(user.personAddressId, user.personPhoneId));
            if (z == false)
            {
                CreateUser(user.personAddressId, user.personPhoneId);
                user.id = udao.GetUser(user.personAddressId, user.personPhoneId).id;
            }
            else
            {
                udao.CreateUser(user.personAddressId, user.personPhoneId);
            }
            return user.id;
        }
        public int SetPersonAddressId(int personId, int addressId, int typeId)
        {
            PersonAddressDAO dao = new PersonAddressDAO();
            int y = GetPersonAddressId(personId, addressId, typeId);
            bool x = dao.DoesPersonAddressExist(y);
            if (x == false)
            {
                dao.CreatePersonAddress(personId, addressId, typeId);
                y = GetPersonAddressId(personId, addressId, typeId);
            }
            return y;
        }
        public int SetCatAndTypeId(int categoryId, int typeId)
        {
            EquipmentDAO dao = new EquipmentDAO();
            int y = GetEquipmentId(categoryId, typeId);
            bool x = dao.DoesEquipmentExist(y);
            if (x == false)
            {
                dao.CreateEquipment(categoryId, typeId);
                y = GetEquipmentId(categoryId, typeId);
            }
            return y;
        }
        public int SetPersonPhoneId(int personId, int phoneId, int typeId)
        {
            PersonPhoneDAO dao = new PersonPhoneDAO();
            int y = GetPersonPhoneId(personId, phoneId, typeId);
            bool x = dao.DoesPersonPhoneExist(y);
            if (x == false)
            {
                dao.CreatePersonPhone(personId, phoneId, typeId);
                y = GetPersonAddressId(personId, phoneId, typeId);
            }
            return y;
        }
        public void DeleteOrder(int id)
        {
            OrderDAO dao = new OrderDAO();
            dao.DeleteOrder(id);
        }
    }
}

