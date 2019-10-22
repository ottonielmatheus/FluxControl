import React, { Component } from 'react';
import styled from 'styled-components';

import { Icon } from '../Shared/Icon';
import { Context } from '../Shared/Context';

const Container = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
  height: 40px;
  background-color: #1C233B;

  div:first-child {
    padding-left: 10px;
    display: flex;
    align-items: center;
    height: 100%;
  }
`

const Text = styled.p`
  margin: 0 0 0 5px;
`

const UserType = styled(Text)`
  font-size: 16px;
  font-weight: 600;
  color: #FFFFFF;
`

const UserName = styled(Text)`
  font-size: 12px;
  font-weight: 500;
  color: #FFFFFF;
`

const Buttons = styled.div`
  height: 100%;
  display: flex;
`

const Button = styled.button`
  background-color: ${props => props.signout ? '#E26060' : '#697F9F'}
  height: 100%;
  width: 40px;
  display: flex;
  justify-content: center;
  align-items: center;
  outline: none;
  border:none;
`

export class NavUserInfo extends Component {
  displayName = NavUserInfo.name

  render() {

    return (
      <Context.Consumer>
        {({ user, userTypes }) =>
          <Container>
            <div>
              <Icon icon="USER_DEFAULT" height="18px" fill="#9BBAE8" />
              <UserType>{user.name}</UserType>
              <UserName>{userTypes[user.type]}</UserName>
            </div>

            <Buttons>
              {user.type === 2 &&
                <Button>
                <Icon icon="SETTINGS" height="16px" fill="#FFFFFF" />
                </Button>
              }
              <Button signout>
                <Icon icon="EXIT" height="16px" fill="#FFFFFF" />
              </Button>
            </Buttons>
          </Container>
        }
      </Context.Consumer>
    );
  }
}