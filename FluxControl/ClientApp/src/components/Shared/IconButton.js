import React, { Component } from 'react';

import { Button } from './Button';
import { Icon } from './Icon';

export class IconButton extends Component {
  displayName = IconButton.name

  constructor(props) {
    super(props)
    this.state = {
      hover: false
    }

    this.toggleHover = this.toggleHover.bind(this);
  }

  toggleHover() {
    this.setState({ hover: !this.state.hover });
    this.refs.iconchild.hover(!this.state.hover, this.props.color.primary);
  }

  render() {

    return (
      <Button
        onMouseEnter={this.toggleHover}
        onMouseLeave={this.toggleHover}
        margin={this.props.margin}
        color={this.props.color}
        onClick={this.props.onClick}
        padding={'10px 15px'}
      >
        <Icon
          icon={this.props.icon}
          height="16px"
          fill={this.props.color.secondary}
          ref="iconchild"
        />
      </Button>
    );
  }
}