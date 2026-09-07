import { Component, OnInit, inject, signal } from '@angular/core';
import { HealthService } from './core/health.service';
import { ArchitectureReview } from './features/architecture-review/architecture-review';
import { IncidentResponder } from './features/incident-responder/incident-responder';

type ApiStatus = 'checking' | 'online' | 'offline';
type Tab = 'architecture-review' | 'incident-responder';

@Component({
  selector: 'app-root',
  imports: [ArchitectureReview, IncidentResponder],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  private readonly healthService = inject(HealthService);

  protected readonly apiStatus = signal<ApiStatus>('checking');
  protected readonly activeTab = signal<Tab>('architecture-review');

  ngOnInit(): void {
    this.healthService.check().subscribe({
      next: () => this.apiStatus.set('online'),
      error: () => this.apiStatus.set('offline'),
    });
  }

  protected setActiveTab(tab: Tab): void {
    this.activeTab.set(tab);
  }
}
