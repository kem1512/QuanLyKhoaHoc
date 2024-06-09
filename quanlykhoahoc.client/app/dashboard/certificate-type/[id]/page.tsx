"use client";

import CertificateTypeHandler from "../../../../components/Handler/CertificateTypeHandler";

export default function DashboardCertificateTypeUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <CertificateTypeHandler id={id} />;
}
