import React, { Component, Fragment } from 'react';
import { Row, Col } from 'react-bootstrap';
import styled from 'styled-components';

import { UserContext } from '../components/Shared/Context';
import { FormValidator, FormLimiter } from '../components/Shared/Validator';
import { UserController, UserStatus } from '../controllers/User';
import { Input } from '../components/Shared/Input';
import { Button } from '../components/Shared/Button';
import { DataTable } from '../components/Shared/DataTable';
import { Loading } from '../components/Shared/Loading';
import { IconButton } from '../components/Shared/IconButton';
import { Dialog, DialogType } from '../components/Dialog/Dialog';

const Filters = styled.div`
  width: 100%;
  background-color: #F1F1F1;
  padding: 35px 60px;
  height: 40%;
`

const UserTable = styled.div`
  padding: 35px 60px;
  overflow-y: scroll;
  height: 60%;

  ${props => props.loading === 'false' ?
  'table {' +
    'width: 102%;' +
  '}' :
  'display: flex;' +
  'justify-content: center;' +
  'align-items: center;' +
  'height: 50vh;'
  }
`

const Title = styled.h1`
  font-size: 30px;
  margin: 0;
  margin-bottom: 20px;
  color: #414141;
`

const ColSubmit = styled(Col)`
  button {
    margin-top: 14px;
    width: 100%;
  }
`

export class User extends Component {
  displayName = User.name;

  constructor(props) {
    super(props);

    this.usersTypes = {
      0: 'Transparencia',
      1: 'Operador',
      2: 'Gerente',
      3: 'Administrador'
    }

    this.state = {
      dataUsers: {
        id: "UsersTable",
        options: {
          detail: true,
          filter: true
        },
        columns: [
          {
            label: 'Matrícula',
            field: 'registration',
            sort: 'asc',
            width: '15%'
          },
          {
            label: 'Nome',
            field: 'name',
            sort: 'asc',
            width: '30%'
          },
          {
            label: 'Email',
            field: 'email',
            sort: 'asc',
            width: '35%'
          },
          {
            label: 'Tipo',
            field: 'type',
            sort: 'asc',
            width: '20%'
          }
        ],
        rows: []
      },
      form: {
        name: {
          value: null,
          required: true,
          minlength: 3,
          error: null
        },
        registration: {
          value: null,
          required: true,
          minlength: 4,
          onlyNumber: true,
          error: null
        },
        email: {
          value: null,
          required: true,
          minlength: 5,
          email: true,
          error: null
        },
        type: {
          value: null,
          required: true,
          select: true,
          error: null
        }
      },
      dataUsersStatus: UserStatus.LOADING,
      dialog: {
        show: false,
        options: {
          close: () => {
            this.setState(Object.assign(this.state, { dialog: { show: false } }))
          },
          confirm: () => { },
          message: "",
          type: DialogType.CONFIRM
        }
      }
    }

    this.submitForm = this.submitForm.bind(this);
    this.validateForm = this.validateForm.bind(this);
    this.refresh = this.refresh.bind(this);
    this.closeDialog = this.closeDialog.bind(this);
    this.startDialog = this.startDialog.bind(this);
  }

  // FORM -------------------
  submitForm() {
    if (this.validateForm() === 0) {
      this.createUser(this.state.form);
    }
  }

  validateForm() {
    let { form } = this.state;
    let errors = 0;
    for (let key in form) {
      let error = FormValidator(form[key]);
      form[key].error = error;

      if (error) errors++;
    }

    this.setState(Object.assign(this.state, {
      form: form
    }));

    return errors;
  }

  handleValue(event, father) {
    const name = event.target.name;
    let newValue = {
      form: {
        ...this.state.form
      }
    };
    newValue.form[name].value = event.target.value;
    newValue.form[name].value = FormLimiter(newValue.form[name]);
    newValue.form[name].error = FormValidator(newValue.form[name]);

    father.setState(Object.assign(this.state, newValue));
  }

  // DIALOG -------------------
  startDialog(message, type, close, confirm = null) {
    this.setState(Object.assign(this.state, {
      dialog: {
        show: true,
        options: {
          message: message,
          type: type,
          confirm: confirm,
          close: close
        }
      }
    }))
  }

  closeDialog() {
    this.setState(Object.assign(this.state, {
      dialog: {
        show: false
      }
    }))
  }

  // CREATE -------------------
  async createUser(user) {
    const userController = new UserController();
    let response = await userController.Create({
      name: user.name.value,
      registration: user.registration.value,
      email: user.email.value,
      type: user.type.value
    });

    this.startDialog(
      response,
      response.status ? DialogType.DONE : DialogType.ERROR,
      () => this.closeDialog()
    )

    this.refresh(this);
  }

  // EXCLUDE -------------------
  async excludeUser(id) {
    const userController = new UserController();
    let response = await userController.Exclude(id);

    this.refresh(this);
  }

  // LOAD -------------------
  async refresh(father) {
    this.setState(Object.assign(this.state, {
      dataUsersStatus: UserStatus.LOADING
    }));

    const userController = new UserController();
    let users = await userController.Load();

    users = users.map((user) => {
      let type = this.usersTypes[user.type];

      let detail = (
        <React.Fragment>
          <Button
            color={{ primary: "#FFFFFF", secondary: "#4D7DC5" }}
            margin={'0 20px 0 0'}
            fontsize={'16px'}
            uppercase
          >
            soliciar nova senha
          </Button>
          <IconButton
            color={{ primary: "#FFBA19", secondary: "#FFFFFF" }}
            margin={'0 20px 0 0'}
            icon="EDIT"
          >
          </IconButton>
          <IconButton
            color={{ primary: "#D54470", secondary: "#FFFFFF" }}
            margin={'0 20px 0 0'}
            onClick={() => {
              this.startDialog(
                (
                  <Fragment>
                    <p>Você está prestes a remover o funcionário <strong>{user.name}</strong>.</p>
                    <p>Tem certeza que deseja prosseguir com essa operação</p>
                  </Fragment>
                ),
                DialogType.CONFIRM,
                () => this.closeDialog(),
                () => this.excludeUser(user.id)
              );
            }}
            icon="TRASH"
          >
          </IconButton>
        </React.Fragment>
      )

      return {
        ...user,
        type: type,
        key: user.registration,
        flexright: true,
        detail: detail
      }
    })

    if (users) {
      father.setState(Object.assign(this.state, {
        dataUsers: {
          ...this.state.dataUsers,
          rows: users
        },
        dataUsersStatus: UserStatus.SUCCESS,
        dialog: {
          show: false
        }
      }))
    }
  }

  async componentDidMount() {
    await this.refresh(this);
  }

  render() {

    let userContext = {
      handleValue: (event) => this.handleValue(event, this),
      refresh: () => this.refresh(this)
    }

    return (
      <UserContext.Provider value={userContext}>
        {this.state.dialog.show &&
          <Dialog options={this.state.dialog.options} />
        }
        <Filters>
          <Title> Novo Funcionário </Title>

          <Row>
            <Col lg={8}>
              <Input
                name="name"
                label="Nome:"
                type="text"
                context={UserContext}
                value={this.state.form.name.value}
                error={this.state.form.name.error}
              />
            </Col>
            <Col lg={4}>
              <Input
                name="registration"
                label="Matrícula:"
                type="text"
                context={UserContext}
                value={this.state.form.registration.value}
                error={this.state.form.registration.error}
              />
            </Col>
          </Row>
          <Row>
            <Col lg={5}>
              <Input
                name="email"
                label="Email:"
                type="email"
                context={UserContext}
                value={this.state.form.email.value}
                error={this.state.form.email.error}
              />
            </Col>
            <Col lg={4}>
              <Input
                name="type"
                label="Tipo:"
                type="select"
                context={UserContext}
                value={this.state.form.type.value}
                error={this.state.form.type.error}
              >
                <option value="default"> </option>
                <option value="0">Transparência</option>
                <option value="1">Operador</option>
                <option value="2">Gerente</option>
                <option value="3">Administrador</option>
              </Input>
            </Col>
            <ColSubmit lg={3}>
              <Button
                color={{ primary: "#FFFFFF", secondary: "#29B489" }}
                type="submit"
                disabled={this.state.form.submit}
                uppercase
                onClick={this.submitForm}
              >
                adicionar
              </Button>
            </ColSubmit>
          </Row>
        </Filters>
        <UserTable loading={(this.state.dataUsersStatus === UserStatus.LOADING).toString()}>
          {this.state.dataUsersStatus === UserStatus.LOADING ?
            <Loading />
            :
            <DataTable data={this.state.dataUsers} />
          }
        </UserTable>
      </UserContext.Provider>
    );
  }
}
