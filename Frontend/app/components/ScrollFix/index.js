import { Component } from 'react';

class ScrollFix extends Component {

  componentDidUpdate(prevProps) {
    if (prevProps.location !== this.props.location) {
      window.scrollTo(0, 0);
    }
  }

  render() {
    return this.props.children;
  }
}

export default ScrollFix;
