import React, { Component } from 'react';
import styled, { createGlobalStyle } from 'styled-components'
import { Route, Switch } from 'react-router';
import { Home } from './components/Home';

import { Context } from './components/Shared/Context';

import { NavMenu } from './components/Navigation/Navbar';
import { User } from './screens/User';

const Container = styled.div`
  height: 100%;
  width: 100%;
  padding-left: 300px;
  position: absolute;
  z-index: 10;
`

const GlobalStyle = createGlobalStyle`
  body {
    font-size: 16px;
    background-color: #FFFFFF;
    color: #000000;
    margin: 0;
    padding: 0;
    font-family: 'Roboto', sans-serif;
  }
`

export default class App extends Component {
  displayName = App.name

  GlobalVar = {
    user: {
      name: 'Rodrigo Morales',
      type: 2
    },
    userTypes: {
      0: 'Transparência',
      1: 'Operador',
      2: 'Gerente',
      3: 'Administrador'
    },
    paths: {
      index: '/',
      users: '/Funcionarios'
    }
  }

  render() {
    return (
      <Context.Provider value={this.GlobalVar}>
        <Context.Consumer>
          {({ paths }) =>
            <React.Fragment>
              <GlobalStyle />

              <NavMenu />

              <Container>
                <Switch>
                  <Route exact path={paths.index} component={Home} />
                  <Route path={paths.users} component={User} />
                </Switch>
              </Container>
            </React.Fragment>
          }
        </Context.Consumer>
      </Context.Provider>
    );
  }
}
