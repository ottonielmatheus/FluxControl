import React, { Component } from 'react';
import styled from 'styled-components'

import { NavItem, NavLink } from './NavItem';
import { NavLogo } from './NavLogo';
import { NavUserInfo } from './NavUserInfo';

import { Context } from '../Shared/Context';

const Navbar = styled.nav`
  position: fixed;
  left: 0;
  width: 100%;
  max-width: 300px;
  height: 100%;
  background-color: #E1E1E1;
  z-index: 20;
`

export class NavMenu extends Component {
  displayName = NavMenu.name

  render() {
    return (
      <Context.Consumer>
        {({ paths }) =>
          <Navbar>
            <NavLogo />
            <NavUserInfo />
            <NavItem icon="REGISTER" title="Cadastrar">
              <NavLink icon="USER_REGISTER" title="Funcionário" to={paths.users} />
            </NavItem>
          </Navbar>
        }
      </Context.Consumer>
    );
  }
}
