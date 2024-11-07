import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './Admn/dashboard/dashboard.component';
import { RouterModule } from '@angular/router';
import { CategryComponent } from './Admn/categry/categry.component';
import { ProductComponent } from './Admn/product/product.component';
import { ProjectComponent } from './Admn/project/project.component';
import { TeamComponent } from './Admn/team/team.component';
import { TilerComponent } from './Admn/tiler/tiler.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    CategryComponent,
    ProductComponent,
    ProjectComponent,
    TeamComponent,
    TilerComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,

    RouterModule.forRoot(
      [
        { path: '', redirectTo: 'dashboard/catgory', pathMatch: 'full' },
        {
          path: "dashboard", component: DashboardComponent, children: [
            { path: "catgory", component: CategryComponent },
            { path: "Product", component: ProductComponent },
            { path: "Project", component: ProjectComponent },
            { path: "Team", component: TeamComponent },
            { path: "Tiler", component: TilerComponent },
          ]
        }
      ]
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
