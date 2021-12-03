import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChefListComponent } from './chef-list.component';

describe('ChefListComponent', () => {
  let component: ChefListComponent;
  let fixture: ComponentFixture<ChefListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChefListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChefListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
