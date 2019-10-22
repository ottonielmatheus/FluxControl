import React, { Fragment } from 'react';
import styled from 'styled-components';

import { Icon } from '../Shared/Icon';
import { Button } from '../Shared/Button';

const Body = styled.div`
  position: absolute;
  width: 100%;
  height: 100%;
  left: 0;
  padding-left: 300px;
  background-color: rgba(65, 65, 65, 0.4);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 10;
`

const Container = styled.div`
  background-color: #ECECEC;
  padding: 25px;
  box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
  position: relative;
  width: 700px;
`

const Message = styled.div`
  display: flex;
  justify-content: flex-start;
  align-items: center;
  min-height: 100px;

  span:first-child {
    margin-right: 10px;
  }

  p {
    margin: 0;
  }
`

const Close = styled.button`
  background: none;
  border: none;
  height: 15px;
  width: 15px;
  position: absolute;
  right: 25px;
  top: 25px;
  padding: 0;
  margin: 0;
  cursor: pointer;
  outline: none;
`

const Buttons = styled.div`
  display: flex;
  justify-content: flex-end;
  align-items: center;

  button {
    margin-left: 15px;
  }
`

export const DialogType = {
  ALERT: 1,
  CONFIRM: 2,
  MESSAGE: 3,
  DONE: 4,
  ERROR: 5
}

export class Dialog extends React.Component {

  render() {

    const getIcon = () => {
      switch (this.props.options.type) {
        case DialogType.ALERT:
          return <Icon icon="WARNING" fill="#000000" height="30px" />
        case DialogType.CONFIRM:
          return <Icon icon="WARNING" fill="#000000" height="30px" />
        case DialogType.DONE:
          return <Icon icon="CHECK_CIRCLE" fill="#000000" height="30px" />
        case DialogType.ERROR:
          return <Icon icon="ERROR" fill="#000000" height="30px" />
        default:
          return null;
      }
    }

    const getButtons = () => {
      switch (this.props.options.type) {
        case DialogType.CONFIRM:
          return (
            <Fragment>
              <Button onClick={this.props.options.close} color={{ primary: "#FFFFFF", secondary: "#959595" }}>Cancelar</Button>
              <Button onClick={this.props.options.confirm} color={{ primary: "#FFFFFF", secondary: "#959595" }}>Prosseguir</Button>
            </Fragment>
          );
        default:
          return <Button onClick={this.props.options.close} color={{ primary: "#FFFFFF", secondary: "#959595" }}>OK</Button>
      }
    }

    return (
      <Body>
        <Container>
          <Close onClick={this.props.options.close}><Icon icon="CLOSE" fill="#606060" height="15px" /></Close>
          <Message>
            <span>
              {getIcon()}
            </span>
            <span>{this.props.options.message}</span>
          </Message>
          <Buttons>
            {getButtons()}
          </Buttons>
        </Container>
      </Body>
    );
  }
}