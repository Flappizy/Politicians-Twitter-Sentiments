import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import './Home.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.refreshPage = this.refreshPage.bind(this);
    
  }

  refreshPage() {
    window.location.reload(false);
  }

  render () {
    return (
      <header>
            <Navbar className="navbar-expand-sm header-bg-color navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand onClick={this.refreshPage}><span className="header-bg-color">TwitterCandidateSentiments</span></NavbarBrand>
            </Container>
        </Navbar>
      </header>
    );
  }
}
