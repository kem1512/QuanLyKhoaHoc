"use client";

import CertificateHandler from "../../../../components/Handler/CertificateHandler";

export default function DashboardCertificateUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <CertificateHandler id={id} />;
}
