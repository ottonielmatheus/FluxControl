import axios from 'axios';

export class UserController {
  async Load() {
    try {
        const response = await axios.get('https://localhost:44322/API/User/Load');

      if (response.status === 200) {
        return response.data;
      } else {
        return false;
      }
    } catch (error) {
      console.log(error);
      return false;
    }
  }

  async Create(user) {
    try {
      const response = await axios.post('/API/User/Add', user);
      switch (response.status) {
        case 201:
          return { status: true, message: `A conta de ${user.name} foi criada com sucesso!` }
        case 424:
          return { status: false, message: `Falha ao contactar o serviço de e-mail, conta não criada.` }
        case 304:
          return { status: false, message: `Falha ao criar a conta. Contacte os desenvolvedores` }
        case 500:
          return { status: false, message: `Falha ao criar a conta. Contacte os desenvolvedores` }
        default:
          return { status: false, message: `Falha ao criar a conta. Contacte os desenvolvedores` }
      }
    } catch (error) {
      console.log(error);
      return false;
    }
  }

  async Exclude(id) {
    console.log(id);
    try {
      const response = await axios.delete('/API/User/Remove', { params: { id: id } });
      if (response.status === 200) {
        return response;
      } else {
        return false;
      }
    } catch (error) {
      console.log(error);
      return false;
    }
  }
}

export const UserStatus = {
  LOADING: 1,
  SUCCESS: 2,
  ERRORLOAD: 3
}