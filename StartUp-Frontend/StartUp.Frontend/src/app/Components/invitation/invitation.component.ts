import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { RoleService } from '../role/role.service';

@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrls: ['./invitation.component.css']
})
export class InvitationComponent implements OnInit {

  Roles: Array<any> = [];

  constructor(private roleService: RoleService) {
    this.getAllRoles();
  }

  ngOnInit(): void {
  }

  private getAllRoles() {
    this.roleService.getRoles().forEach(r => {
      this.Roles.push(r);
      console.log("role:", r[2].permission);
    });
  }
}
